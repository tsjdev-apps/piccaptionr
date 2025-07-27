using Avalonia.Platform.Storage;
using PicCaptionr.AvaloniaApp.Services.Interfaces;
using PicCaptionr.Services.Interfaces;

namespace PicCaptionr.AvaloniaApp.Services;

/// <summary>
/// Avalonia implementation of <see cref="IFolderPickerService"/>
/// using OpenFolderDialog to allow the user to select a folder.
/// </summary>
/// <remarks>
/// Constructor requires a reference to <see cref="IWindowService"/>,
/// which provides the main window for dialog parenting.
/// </remarks>
/// <param name="windowService">Service to access the main application window.</param>
public class FolderPickerService(
	IWindowService windowService) : IFolderPickerService
{
	/// <inheritdoc/>
	public async Task<string> GetFolderPathAsync(string title)
	{
		// Get the current window from the window service
		var currentWindow = windowService.GetCurrentWindow();

		// Open a folder picker dialog
		IReadOnlyList<IStorageFolder> folder 
			= await currentWindow.StorageProvider.OpenFolderPickerAsync(
				new FolderPickerOpenOptions
				{
					Title = title,
					AllowMultiple = false
				});

		// If the user selected a folder, return its path
		return folder.Count > 0
			? folder[0].Path.LocalPath
			: string.Empty;
	}
}
