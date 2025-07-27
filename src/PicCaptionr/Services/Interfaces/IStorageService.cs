using PicCaptionr.Models;

namespace PicCaptionr.Services.Interfaces;

/// <summary>
/// Defines methods for storage operations.
/// </summary>
public interface IStorageService
{
	/// <summary>
	///	Creates a directory at the specified folder path.
	/// </summary>
	/// <param name="folderPath">
	/// The path of the folder to create.</param>
	void CreateDirectory(
		string folderPath);

	/// <summary>
	///	Checks if a directory exists at the specified folder path.
	/// </summary>
	/// <param name="folderPath">
	/// The path of the folder to check.</param>
	/// <returns>
	/// True if the directory exists, otherwise false.
	/// </returns>
	bool DirectoryExists(
		string folderPath);

	/// <summary>
	///	Gets the list of picture files in the specified folder path.
	/// </summary>
	/// <param name="folderPath">
	/// The path of the folder to get the picture files from.</param>
	/// <returns>
	/// A list of picture file paths.
	/// </returns>
	List<string> GetPictureFilesInFolder(
		string folderPath);

	/// <summary>
	///	Writes the Instagram items to a file.
	/// </summary>
	/// <param name="fileName">
	/// The name of the file.</param>
	/// <param name="instagramItems">
	/// The list of Instagram items to write.</param>
	void WriteData(
		string fileName, 
		List<InstagramItem> instagramItems);
}