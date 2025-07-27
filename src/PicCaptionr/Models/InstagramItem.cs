namespace PicCaptionr.Models;

/// <summary>
/// Represents an Instagram item.
/// </summary>
public record InstagramItem(

	/// <summary>
	///     Gets or sets the image name.
	/// </summary>
	string ImageName,

	/// <summary>
	///     Gets or sets the image metadata.
	/// </summary>
	ImageMetaDataResponse ImageMetaData,

	/// <summary>
	///     Gets or sets the OpenAI response.
	/// </summary>
	AIResponse OpenAIResponse);
