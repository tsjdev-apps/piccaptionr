using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicCaptionr.Utils;

/// <summary>
/// Provides a centralized collection of static string constants used throughout the application.
/// This includes UI titles, error messages, and process log entries.
/// </summary>
public static class TextStatics
{
	// ───────────── UI Titles ─────────────

	/// <summary>
	/// Title shown when prompting the user to select the picture folder.
	/// </summary>
	public const string PictureFolderPathTitle
		= "Please select the folder containing your pictures";

	/// <summary>
	/// Title shown when prompting the user to select the output folder.
	/// </summary>
	public const string OutputFolderPathTitle
		= "Please select the output folder";

	/// ───────────── UI Prompts ─────────────

	/// <summary>
	/// Prompts the user to enter the path to the folder containing vacation pictures.
	/// </summary>
	public const string PictureFolderPathPrompt =
		"Please insert the path to your vacation pictures:";

	/// <summary>
	/// Prompts the user to select a language for the Instagram captions.
	/// </summary>
	public const string LanguageSelectionPrompt =
		"Please select the [yellow]language[/] of the desired Instagram captions:";

	/// <summary>
	/// Prompts the user to enter additional information to be included in the captions.
	/// </summary>
	public const string AdditionalInformationPrompt =
		"Please insert any [yellow]additional information[/] you want to add to the captions:";

	/// <summary>
	/// Prompts the user to choose their AI host (e.g., OpenAI or Azure OpenAI).
	/// </summary>
	public const string HostSelectionPrompt =
		"Please select your [yellow]host[/]:";

	/// <summary>
	/// Prompts the user to enter their OpenAI API key.
	/// </summary>
	public const string OpenAIKeyPrompt =
		"Please insert your [yellow]OpenAI[/] API key:";

	/// <summary>
	/// Prompts the user to select the OpenAI language model to use.
	/// </summary>
	public const string OpenAIModelSelectionPrompt =
		"Please select the [yellow]Language Model[/] you want to use:";

	/// <summary>
	/// Prompts the user to enter their Azure OpenAI API key.
	/// </summary>
	public const string AzureOpenAIKeyPrompt =
		"Please insert your [yellow]Azure OpenAI[/] API key:";

	/// <summary>
	/// Prompts the user to enter their Azure OpenAI endpoint.
	/// </summary>
	public const string AzureOpenAIEndpointPrompt =
		"Please insert your [yellow]Azure OpenAI[/] endpoint:";

	/// <summary>
	/// Prompts the user to specify the deployment name for the Azure OpenAI model.
	/// </summary>
	public const string AzureOpenAIDeploymentPrompt =
		"Please insert the [yellow]Language Model Name[/] you want to use:";

	/// <summary>
	/// Prompts the user to enter the output folder path for the result file.
	/// </summary>
	public const string OutputPathPrompt =
		"Please insert the output path:";


	// ───────────── Error Messages ─────────────

	/// <summary>
	/// Error message shown when no picture folder was selected.
	/// </summary>
	public const string NoPictureFileSelectedError
		= "No Picture Folder selected.";

	/// <summary>
	/// Error message shown when no output folder was selected.
	/// </summary>
	public const string NoOutputFolderError
		= "No Output Folder selected.";

	/// <summary>
	/// Error message shown when the selected folder path does not exist.
	/// </summary>
	public const string FolderDoesNotExistsError
		= "The specified folder does not exist!";

	/// <summary>
	/// Error message shown when no image files were found in the selected folder.
	/// </summary>
	public const string NoPicturesInFolderError
		= "No picture files found in the specified folder.";

	/// <summary>
	/// Error message shown when the specified folder path does not exist.
	/// </summary>
	public const string FolderDoesNotExistError
		= "The specified folder does not exist!\r\n";

	// ───────────── Process Messages ─────────────

	/// <summary>
	/// Log message shown when the picture folder is selected.
	/// </summary>
	public const string PictureFolderSelectedProcessMessage
		= "Picture folder was selected.";

	/// <summary>
	/// Log message shown when the output folder is selected.
	/// </summary>
	public const string OutputFolderSelectedProcessMessage
		= "Output Folder selected.";

	/// <summary>
	/// Log message shown when the image processing starts.
	/// </summary>
	public const string StartImageProcessMessage
		= "Start image processing...";

	/// <summary>
	/// Log message shown when EXIF data is being extracted from the image.
	/// </summary>
	public const string ExtractExifDataImageProcessMessage
		= "Extract EXIF data from image...";

	/// <summary>
	/// Log message shown when the image is being resized.
	/// </summary>
	public const string ResizeImageProcessMessage
		= "Resize image...";

	/// <summary>
	/// Log message shown when an AI vision request is being made.
	/// </summary>
	public const string MakeVisionRequestProcessMessage
		= "Making AI vision request...";

	/// <summary>
	/// Separator line used in process logging.
	/// </summary>
	public const string SeparatorProcessMessage
		= "---";

	/// <summary>
	/// Log message shown when the result is being written to the output file.
	/// </summary>
	public const string WriteOutputProcessMessage
		= "Writing to output file...";

	/// <summary>
	/// Log message shown when all images have been processed.
	/// </summary>
	public const string AllPhotosProcessedProcessMessage
		= "All images were processed.";

	/// <summary>
	/// Message shown at the end of processing, prompting the user to exit.
	/// </summary>
	public const string FinishProcessMessage
		= "All images where processed. Please press any key to exit the program.\r\n";
}

