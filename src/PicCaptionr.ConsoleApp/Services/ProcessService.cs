using PicCaptionr.ConsoleApp.Services.Interfaces;
using PicCaptionr.Models;
using PicCaptionr.Services.Interfaces;
using PicCaptionr.Utils;

namespace PicCaptionr.ConsoleApp.Services;

/// <summary>
/// Orchestrates the processing pipeline.
/// 
/// Combines multiple services such as AI chat, image manipulation,
/// console interactions, and file storage to process image files
/// and generate captions.
/// </summary>
/// <param name="chatService">
/// Service for interacting with OpenAI or Azure OpenAI.</param>
/// <param name="consoleService">
/// Service for reading/writing messages to the console.</param>
/// <param name="imageService">
/// Service for loading and transforming images.</param>
/// <param name="processTrackerService">
/// Service for logging and tracking steps during processing.</param>
/// <param name="promptService">
/// Service for creating prompts based on image metadata and instructions.</param>
/// <param name="storageService">
/// Service for accessing and writing local files (e.g., images, output).</param>

public class ProcessService(
	IChatService chatService,
	IConsoleService consoleService,
	IImageService imageService,
	IProcessTrackerService processTrackerService,
	IPromptService promptService,
	IStorageService storageService) : IProcessService
{
	/// <inheritdoc/>
	public async Task StartProcessAsync()
	{
		InitializeChatClient();

		string pictureFolderInputPath
			= GetValidatedFolderPath(
				TextStatics.PictureFolderPathPrompt);

		if (string.IsNullOrWhiteSpace(pictureFolderInputPath))
		{
			return;
		}

		List<string> pictureFiles
			= storageService.GetPictureFilesInFolder(
				pictureFolderInputPath);

		if (pictureFiles.Count == 0)
		{
			consoleService.WriteError(
				TextStatics.NoPicturesInFolderError);
			return;
		}

		string outputPath
			= GetValidatedOutputFolderPath();

		if (string.IsNullOrWhiteSpace(outputPath))
		{
			return;
		}

		string language
			= consoleService.SelectFromOptions(
				[Statics.EnglishLanguage,
				Statics.GermanLanguage,
				Statics.SpanishLanguage],
				TextStatics.LanguageSelectionPrompt);

		string? additionalInformation
			= consoleService.GetStringFromConsole(
				TextStatics.AdditionalInformationPrompt, true, false);

		consoleService.ShowHeader();

		List<InstagramItem> instagramItems = [];

		foreach (string pictureFile in pictureFiles)
		{
			try
			{
				processTrackerService.TrackProcess(
					$"{TextStatics.StartImageProcessMessage}{Environment.NewLine}");

				processTrackerService.TrackProcess(
					$"{TextStatics.ExtractExifDataImageProcessMessage}{Environment.NewLine}");

				ImageMetaDataResponse imageMetaData
					= imageService.ExtractImageMetadata(pictureFile);

				processTrackerService.TrackProcess(
					$"{TextStatics.ResizeImageProcessMessage}{Environment.NewLine}");

				byte[] resizedImage
					= imageService.ResizeImage(
						pictureFile,
						Statics.ImageWidth,
						Statics.ImageHeight);

				string fileExtension
					= Path.GetExtension(pictureFile)?
						.TrimStart('.')
						.ToLowerInvariant() ?? "jpg";

				string prompt
					= promptService.GetInstagramCaptionPrompt(
						language,
						imageMetaData,
						additionalInformation);

				processTrackerService.TrackProcess(
					$"{TextStatics.MakeVisionRequestProcessMessage}{Environment.NewLine}");

				AIResponse openAIResponse = await chatService.MakeVisionRequestAsync(
					resizedImage,
					fileExtension,
					prompt);

				instagramItems.Add(
					new InstagramItem(
						pictureFile,
						imageMetaData,
						openAIResponse));

				processTrackerService.TrackProcess(
					$"{TextStatics.SeparatorProcessMessage}{Environment.NewLine}");
			}
			catch (Exception ex)
			{
				consoleService.WriteError(ex.Message);
			}
		}

		string outputFile
			= Path.Combine(
				outputPath,
				$"piccaptionr-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.txt");

		processTrackerService.TrackProcess(
			$"{TextStatics.WriteOutputProcessMessage}{Environment.NewLine}");

		storageService.WriteData(
			outputFile,
			instagramItems);

		processTrackerService.TrackProcess(
			$"{TextStatics.FinishProcessMessage}{Environment.NewLine}");
	}

	/// <summary>
	/// Initializes the chat client by asking the user whether to use OpenAI or Azure OpenAI
	/// and requesting the necessary credentials and configuration.
	/// </summary>
	void InitializeChatClient()
	{
		string chatHost
			= consoleService.SelectFromOptions(
				[Statics.OpenAiKey, Statics.AzureOpenAiKey],
				TextStatics.HostSelectionPrompt);

		if (chatHost == Statics.OpenAiKey)
		{
			string key
				= consoleService.GetStringFromConsole(
					TextStatics.OpenAIKeyPrompt)!;

			string model = consoleService.SelectFromOptions(
				[Statics.GPT41Key, Statics.GPT41MiniKey, Statics.GPT41NanoKey,
				 Statics.GPT4oKey, Statics.GPT4oMiniKey],
				TextStatics.OpenAIModelSelectionPrompt);

			chatService.InitOpenAIChatClient(
				key,
				model);
		}
		else
		{
			string endpoint
				= consoleService.GetUrlFromConsole(
					TextStatics.AzureOpenAIEndpointPrompt);

			string key
				= consoleService.GetStringFromConsole(
					TextStatics.AzureOpenAIKeyPrompt)!;

			string deployment
				= consoleService.GetStringFromConsole(
					TextStatics.AzureOpenAIDeploymentPrompt)!;

			chatService.InitAzureOpenAIChatClient(
				endpoint,
				key,
				deployment);
		}
	}

	/// <summary>
	/// Prompts the user for a folder path and validates that it exists.
	/// </summary>
	/// <param name="prompt">
	/// The message shown to the user.</param>
	/// <returns>
	/// The folder path if valid, otherwise an empty string.
	/// </returns>
	string GetValidatedFolderPath(string prompt)
	{
		string folderPath
			= consoleService.GetStringFromConsole(prompt)!;

		consoleService.ShowHeader();

		if (!storageService.DirectoryExists(folderPath))
		{
			consoleService.WriteError(
				TextStatics.FolderDoesNotExistError);

			return string.Empty;
		}

		return folderPath;
	}

	/// <summary>
	/// Prompts the user for an output folder and creates it if it does not exist.
	/// </summary>
	/// <returns>The valid output folder path.</returns>
	string GetValidatedOutputFolderPath()
	{
		string outputPath
			= consoleService.GetStringFromConsole(
				TextStatics.OutputPathPrompt)!;

		consoleService.ShowHeader();

		if (!storageService.DirectoryExists(outputPath))
		{
			storageService.CreateDirectory(outputPath);
		}

		return outputPath;
	}
}
