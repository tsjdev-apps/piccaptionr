namespace PicCaptionr.Services.Interfaces;

/// <summary>
/// Provides a service for displaying a 
/// folder picker dialog and returning 
/// the selected folder path.
/// </summary>
public interface IFolderPickerService
{
	/// <summary>
	/// Opens a folder selection dialog and 
	/// returns the path of the selected folder.
	/// </summary>
	/// <param name="title">
	/// The title displayed on the folder picker dialog.</param>
	/// <returns>
	/// A task that represents the asynchronous operation. 
	/// The task result contains the selected folder path, 
	/// or an empty string if canceled.</returns>
	Task<string> GetFolderPathAsync(string title);
}