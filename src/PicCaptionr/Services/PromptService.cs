using System.Globalization;
using System.Text;
using PicCaptionr.Models;
using PicCaptionr.Services.Interfaces;

namespace PicCaptionr.Services;

/// <summary>
/// Provides logic for generating or formatting prompts 
/// to be sent to an AI model for image captioning.
/// </summary>
public class PromptService : IPromptService
{
	/// <inheritdoc/>
	public string GetInstagramCaptionPrompt(
		string language,
		ImageMetaDataResponse imageMetaData,
		string? additionalInformation)
	{
		if (string.IsNullOrWhiteSpace(language))
		{
			throw new ArgumentException("Language must not be null or empty.", nameof(language));
		}

		StringBuilder promptBuilder = new();

		promptBuilder.AppendLine($"Create an engaging caption in {language} for an Instagram post based on the provided image.");
		promptBuilder.AppendLine("Try to recognize notable buildings, landmarks, or environmental details.");
		promptBuilder.AppendLine("Include at least 10 creative and relevant hashtags.");

		AppendIfAvailable(promptBuilder, imageMetaData);
		AppendAdditionalInformation(promptBuilder, additionalInformation);

		return promptBuilder.ToString().Trim();
	}

	/// <summary>
	/// Appends metadata such as capture date and GPS coordinates to the prompt, if available.
	/// </summary>
	/// <param name="builder">The StringBuilder used to construct the prompt.</param>
	/// <param name="data">The image metadata to evaluate.</param>
	static void AppendIfAvailable(StringBuilder builder, ImageMetaDataResponse data)
	{
		if (data.CaptureDate is not null)
		{
			builder.AppendLine($"The image was taken on {data.CaptureDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}.");
		}

		if (data.Latitude is not null)
		{
			builder.AppendLine($"Latitude: {data.Latitude.Value.ToString(CultureInfo.InvariantCulture)}.");
		}

		if (data.Longitude is not null)
		{
			builder.AppendLine($"Longitude: {data.Longitude.Value.ToString(CultureInfo.InvariantCulture)}.");
		}
	}

	/// <summary>
	/// Appends any additional user-provided information to the prompt, 
	/// if it is not null or empty.
	/// </summary>
	/// <param name="builder">The StringBuilder used to construct the prompt.</param>
	/// <param name="info">Optional additional information provided by the user.</param>
	static void AppendAdditionalInformation(StringBuilder builder, string? info)
	{
		if (!string.IsNullOrWhiteSpace(info))
		{
			builder.AppendLine($"Also consider this additional information: {info.Trim()}");
		}
	}
}
