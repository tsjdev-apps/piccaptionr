using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PicCaptionr.Models;
using PicCaptionr.Services.Interfaces;
using PicCaptionr.Utils;

namespace PicCaptionr.WPFApp.ViewModels;

/// <summary>
/// Main view model for the WPF UI.
/// Handles user interactions and orchestrates the image captioning process.
/// </summary>
public partial class MainWindowViewModel(
	IActivityLogService activityLogService,
	IChatService chatService,
	IFolderPickerService filePickerService,
	IImageService imageService,
	IProcessTrackerService processTrackerService,
	IPromptService promptService,
	IStorageService storageService) : ObservableObject
{
	/// <summary>
	/// Indicates whether the image processing is currently running.
	/// Used to disable UI elements during the process.
	/// </summary>
	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(SelectPictureFolderCommand))]
	[NotifyCanExecuteChangedFor(nameof(SelectOutputFolderCommand))]
	bool isProcessing = false;

	/// <summary>
	/// Available host options: OpenAI or Azure OpenAI.
	/// </summary>
	[ObservableProperty]
	List<string> hostOptions
		= [Statics.OpenAiKey, Statics.AzureOpenAiKey];

	/// <summary>
	/// Selected host (OpenAI or Azure OpenAI).
	/// </summary>
	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(ProcessImagesCommand))]
	string selectedHost
		= Statics.OpenAiKey;

	/// <summary>
	/// Available deployment model names.
	/// </summary>
	[ObservableProperty]
	List<string> deploymentNames
		= [Statics.GPT41Key, Statics.GPT41MiniKey, Statics.GPT41NanoKey,
			Statics.GPT4oKey, Statics.GPT4oMiniKey];

	/// <summary>
	/// The selected model (deployment) to use.
	/// </summary>
	[ObservableProperty]
	string selectedDeploymentName
		= Statics.DefaultDeploymentName;

	/// <summary>
	/// Supported output languages for the generated captions.
	/// </summary>
	[ObservableProperty]
	List<string> languages
		= [Statics.EnglishLanguage, Statics.GermanLanguage,
			Statics.SpanishLanguage];

	/// <summary>
	/// The selected language for Instagram captions.
	/// </summary>
	[ObservableProperty]
	string selectedLanguage
		= Statics.DefaultLanguage;

	/// <summary>
	/// Additional user-defined information to include in the AI prompt.
	/// </summary>
	[ObservableProperty]
	string? additionalInformation;

	/// <summary>
	/// The API key for OpenAI.
	/// </summary>
	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(ProcessImagesCommand))]
	string? openAIApiKey;

	/// <summary>
	/// The API key for Azure OpenAI.
	/// </summary>
	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(ProcessImagesCommand))]
	string? azureOpenAIApiKey;

	/// <summary>
	/// The endpoint URL for Azure OpenAI (required if Azure is selected).
	/// </summary>
	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(ProcessImagesCommand))]
	string? azureOpenAIEndpoint;

	/// <summary>
	/// The deployment name for Azure OpenAI.
	/// </summary>
	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(ProcessImagesCommand))]
	string? deploymentName;

	/// <summary>
	/// Path to the folder containing image files to process.
	/// </summary>
	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(ProcessImagesCommand))]
	string? pictureFolderPath;

	/// <summary>
	/// Path to the folder where output should be saved.
	/// </summary>
	[ObservableProperty]
	[NotifyCanExecuteChangedFor(nameof(ProcessImagesCommand))]
	string? outputFolderPath;

	/// <summary>
	/// The log of process messages for binding to the UI.
	/// </summary>
	public ObservableCollection<string> ActivityLog
		=> activityLogService.GetEntries();

	/// <summary>
	/// Opens a folder picker dialog for selecting the input picture folder.
	/// </summary>
	[RelayCommand(
		AllowConcurrentExecutions = false,
		CanExecute = nameof(CanSelectFolder))]
	async Task SelectPictureFolder()
	{
		PictureFolderPath
			= await filePickerService.GetFolderPathAsync(
				TextStatics.PictureFolderPathTitle);

		if (string.IsNullOrEmpty(PictureFolderPath))
		{
			processTrackerService.TrackProcess(
				TextStatics.NoPictureFileSelectedError);
		}
		else
		{
			processTrackerService.TrackProcess(
				TextStatics.PictureFolderSelectedProcessMessage);
		}
	}

	/// <summary>
	/// Opens a folder picker dialog for selecting the output folder.
	/// </summary>
	[RelayCommand(
		AllowConcurrentExecutions = false,
		CanExecute = nameof(CanSelectFolder))]
	async Task SelectOutputFolder()
	{
		OutputFolderPath
			= await filePickerService.GetFolderPathAsync(
				TextStatics.OutputFolderPathTitle);

		if (string.IsNullOrEmpty(OutputFolderPath))
		{
			processTrackerService.TrackProcess(
				TextStatics.NoOutputFolderError);
		}
		else
		{
			processTrackerService.TrackProcess(
				TextStatics.OutputFolderSelectedProcessMessage);
		}
	}

	/// <summary>
	/// Main command to process all images and generate AI captions.
	/// </summary>
	[RelayCommand(
		AllowConcurrentExecutions = false,
		CanExecute = nameof(CanProcessImages))]
	async Task ProcessImagesAsync()
	{
		IsProcessing = true;

		if (!storageService.DirectoryExists(PictureFolderPath!))
		{
			processTrackerService.TrackProcess(
				TextStatics.FolderDoesNotExistsError);
			IsProcessing = false;
			return;
		}

		List<string> pictureFiles
			= storageService.GetPictureFilesInFolder(PictureFolderPath!);

		if (pictureFiles.Count == 0)
		{
			processTrackerService.TrackProcess(
				TextStatics.NoPicturesInFolderError);
			IsProcessing = false;
			return;
		}

		List<InstagramItem> instagramItems = [];

		foreach (string pictureFile in pictureFiles)
		{
			try
			{
				processTrackerService.TrackProcess(
					TextStatics.StartImageProcessMessage);

				processTrackerService.TrackProcess(
					TextStatics.ExtractExifDataImageProcessMessage);

				ImageMetaDataResponse imageMetaData
					= imageService.ExtractImageMetadata(pictureFile);

				processTrackerService.TrackProcess(
					TextStatics.ResizeImageProcessMessage);

				byte[] resizedImage
					= imageService.ResizeImage(
						pictureFile,
						Statics.ImageWidth,
						Statics.ImageHeight);

				string prompt
					= promptService.GetInstagramCaptionPrompt(
						SelectedLanguage,
						imageMetaData,
						AdditionalInformation);

				processTrackerService.TrackProcess(
					TextStatics.MakeVisionRequestProcessMessage);

				// Initialize Chat Client
				if (SelectedHost == Statics.OpenAiKey)
				{
					chatService.InitOpenAIChatClient(
						OpenAIApiKey!,
						SelectedDeploymentName);
				}
				else if (SelectedHost == Statics.AzureOpenAiKey)
				{
					chatService.InitAzureOpenAIChatClient(
						AzureOpenAIApiKey!,
						AzureOpenAIEndpoint!,
						DeploymentName!);
				}

				// Call Vision API
				AIResponse aiResponse
					= await chatService.MakeVisionRequestAsync(
						resizedImage,
						Path.GetFileName(pictureFile),
						prompt);

				instagramItems.Add(
					new InstagramItem(
						pictureFile,
						imageMetaData,
						aiResponse));

				processTrackerService.TrackProcess(
					TextStatics.SeparatorProcessMessage);
			}
			catch (Exception ex)
			{
				processTrackerService.TrackProcess(
					ex.Message);
			}
		}

		processTrackerService.TrackProcess(
			TextStatics.WriteOutputProcessMessage);

		string outputFile
			= $"{OutputFolderPath}/piccaptionr_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";

		storageService.WriteData(outputFile, instagramItems);

		processTrackerService.TrackProcess(
			TextStatics.AllPhotosProcessedProcessMessage);
		IsProcessing = false;
	}

	/// <summary>
	/// Opens a given URL in the system browser.
	/// </summary>
	[RelayCommand]
	static void OpenWebSite(string link)
	{
		ProcessStartInfo processStartInfo = new(link) { UseShellExecute = true };
		Process.Start(processStartInfo);
	}

	/// <summary>
	/// Determines whether the image processing command can be executed.
	/// </summary>
	bool CanProcessImages()
	{
		if (SelectedHost == Statics.OpenAiKey)
		{
			return !string.IsNullOrEmpty(OpenAIApiKey)
				&& !string.IsNullOrEmpty(PictureFolderPath)
				&& !string.IsNullOrEmpty(OutputFolderPath);
		}
		else if (SelectedHost == Statics.AzureOpenAiKey)
		{
			return !string.IsNullOrEmpty(AzureOpenAIApiKey)
				&& !string.IsNullOrEmpty(AzureOpenAIEndpoint)
				&& !string.IsNullOrEmpty(DeploymentName)
				&& !string.IsNullOrEmpty(PictureFolderPath)
				&& !string.IsNullOrEmpty(OutputFolderPath);
		}

		return false;
	}

	/// <summary>
	/// Determines whether folder selection commands can be executed.
	/// </summary>
	bool CanSelectFolder()
		=> !IsProcessing;
}
