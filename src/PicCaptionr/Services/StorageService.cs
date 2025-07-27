using Newtonsoft.Json;
using PicCaptionr.Models;
using PicCaptionr.Services.Interfaces;

namespace PicCaptionr.Services;

/// <summary>
/// Provides file system operations related to 
/// storing and retrieving files.
/// </summary>
public class StorageService : IStorageService
{
	/// <summary>
	/// Supported file extensions for image files.
	/// Used to filter files when searching for images in a folder.
	/// </summary>
	readonly List<string> imageExtensions
		= [".jpg", ".jpeg", ".png", ".gif", ".bmp"];

	/// <inheritdoc/>
	public void CreateDirectory(
		string folderPath)
	{
		if (string.IsNullOrWhiteSpace(folderPath))
		{
			throw new ArgumentException(
				"Folder path must not be null or empty.",
				nameof(folderPath));
		}

		Directory.CreateDirectory(folderPath);
	}

	/// <inheritdoc/>
	public bool DirectoryExists(
		string folderPath)
	{
		return Directory.Exists(folderPath);
	}

	/// <inheritdoc/>
	public List<string> GetPictureFilesInFolder(
		string folderPath)
	{
		if (string.IsNullOrWhiteSpace(folderPath))
		{
			throw new ArgumentException(
				"Folder path must not be null or empty.",
				nameof(folderPath));
		}

		if (!Directory.Exists(folderPath))
		{
			throw new DirectoryNotFoundException(
				$"The directory '{folderPath}' does not exist.");
		}

		try
		{
			return [.. Directory
				.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
				.Where(file => imageExtensions.Any(ext => file.EndsWith(ext, StringComparison.OrdinalIgnoreCase)))];
		}
		catch (UnauthorizedAccessException ex)
		{
			throw new IOException(
				"Access to one or more directories was denied.",
				ex);
		}
		catch (Exception ex)
		{
			throw new IOException(
				"An error occurred while retrieving image files.",
				ex);
		}
	}

	/// <inheritdoc/>
	public void WriteData(
		string fileName,
		List<InstagramItem> instagramItems)
	{
		if (string.IsNullOrWhiteSpace(fileName))
		{
			throw new ArgumentException(
				"Filename must not be null or empty.",
				nameof(fileName));
		}

		if (instagramItems is null || instagramItems.Count == 0)
		{
			throw new ArgumentException(
				"No Instagram items to write.",
				nameof(instagramItems));
		}

		try
		{
			JsonSerializerSettings settings = new()
			{
				Formatting = Formatting.Indented,
				StringEscapeHandling = StringEscapeHandling.Default
			};

			string json = JsonConvert.SerializeObject(instagramItems, settings);
			File.WriteAllText(fileName, json);
		}
		catch (IOException ioEx)
		{
			throw new IOException(
				"An error occurred while writing data to the file.",
				ioEx);
		}
	}
}
