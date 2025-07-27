using PicCaptionr.Models;

namespace PicCaptionr.Services.Interfaces;

/// <summary>
/// Defines methods for image processing operations.
/// </summary>
public interface IImageService
{
	/// <summary>
	///	Extracts the metadata of the image from the specified file path.
	/// </summary>
	/// <param name="filePath">
	/// The path of the image file.</param>
	/// <returns>
	/// An instance of ImageMetaDataResponse containing the image metadata.
	/// </returns>
	ImageMetaDataResponse ExtractImageMetadata(
		string filePath);

	/// <summary>
	///	Resizes the image at the specified path to the given width and height.
	/// </summary>
	/// <param name="path">
	/// The path of the image file.</param>
	/// <param name="width">
	/// The desired width of the resized image.</param>
	/// <param name="height">
	/// The desired height of the resized image.</param>
	/// <returns>
	/// The byte array containing the resized image data.
	/// </returns>
	byte[] ResizeImage(
		string path,
		int width,
		int height);
}