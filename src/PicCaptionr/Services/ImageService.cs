using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using PicCaptionr.Models;
using PicCaptionr.Services.Interfaces;
using SkiaSharp;
using Directory = MetadataExtractor.Directory;

namespace PicCaptionr.Services;

/// <summary>
/// Provides functionality for handling image-related operations 
/// such as loading, conversion, and preparation for AI processing.
/// </summary>
public class ImageService : IImageService
{
	/// <inheritdoc/>
	public ImageMetaDataResponse ExtractImageMetadata(string filePath)
	{
		ValidateFilePath(filePath);

		try
		{
			IReadOnlyList<Directory> directories
				= ImageMetadataReader.ReadMetadata(filePath);

			DateTime? captureDate = directories
				.OfType<ExifSubIfdDirectory>()
				.FirstOrDefault()?
				.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);

			GeoLocation? location = directories
				.OfType<GpsDirectory>()
				.FirstOrDefault()?
				.GetGeoLocation();

			return new ImageMetaDataResponse(
				captureDate,
				location?.Latitude,
				location?.Longitude);
		}
		catch (ImageProcessingException ex)
		{
			throw new InvalidOperationException("Failed to read image metadata.", ex);
		}
	}

	/// <inheritdoc/>
	public byte[] ResizeImage(string path, int width, int height)
	{
		ValidateFilePath(path);

		using SKBitmap originalBitmap = SKBitmap.Decode(path)
			?? throw new InvalidOperationException(
				"Failed to decode image.");

		using SKBitmap resizedBitmap = new(new SKImageInfo(width, height));
		originalBitmap.ScalePixels(resizedBitmap, SKSamplingOptions.Default);

		using SKImage image = SKImage.FromBitmap(resizedBitmap);
		using SKData data = image.Encode(SKEncodedImageFormat.Jpeg, 75);

		return data.ToArray();
	}

	/// <summary>
	/// Validates the file path for an image.
	/// </summary>
	/// <param name="path">The path to the image.</param>
	/// <exception cref="ArgumentException"></exception>
	/// <exception cref="FileNotFoundException"></exception>
	static void ValidateFilePath(string path)
	{
		if (string.IsNullOrWhiteSpace(path))
		{
			throw new ArgumentException(
				"File path must not be null or empty.", nameof(path));
		}

		if (!File.Exists(path))
		{
			throw new FileNotFoundException(
				"Image file not found.", path);
		}
	}
}
