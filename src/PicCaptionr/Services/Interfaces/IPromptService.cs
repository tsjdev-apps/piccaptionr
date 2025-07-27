using PicCaptionr.Models;

namespace PicCaptionr.Services.Interfaces;

/// <summary>
/// Defines methods for generating prompts for Instagram captions.
/// </summary>
public interface IPromptService
{
	/// <summary>
	///	Gets the Instagram caption prompt for the 
	///	specified language and image metadata.
	/// </summary>
	/// <param name="language">
	/// The language of the caption.</param>
	/// <param name="imageMetaData">
	/// The metadata of the image.</param>
	/// <param name="additionalInformation">
	/// The additional information for the image.</param>
	/// <returns>
	/// The Instagram caption prompt.
	/// </returns>
	string GetInstagramCaptionPrompt(
		string language, 
		ImageMetaDataResponse imageMetaData, 
		string? additionalInformation);
}