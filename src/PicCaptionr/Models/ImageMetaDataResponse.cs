namespace PicCaptionr.Models;

/// <summary>
/// Represents the response containing image metadata.
/// </summary>
public record ImageMetaDataResponse(

	/// <summary>
	/// Gets or sets the capture date of the image.
	/// </summary>
	DateTime? CaptureDate,

	/// <summary>
	/// Gets or sets the latitude of the image location.
	/// </summary>
	double? Latitude,

	/// <summary>
	/// Gets or sets the longitude of the image location.
	/// </summary>
	double? Longitude);
