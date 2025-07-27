using Microsoft.WindowsAPICodePack.Dialogs;
using PicCaptionr.Services.Interfaces;

namespace PicCaptionr.WPFApp.Services;

/// <summary>
/// WPF implementation of <see cref="IFolderPickerService"/> 
/// using CommonOpenFileDialog to allow the user to select 
/// a folder.
/// </summary>
public class FolderPickerService : IFolderPickerService
{
	/// <inheritdoc/>
	public async Task<string> GetFolderPathAsync(string title)
	{
		// Ensure async method signature
		await Task.Yield();

		// Use CommonOpenFileDialog to select a folder
		using CommonOpenFileDialog dialog = new()
		{
			IsFolderPicker = true,
			Title = title
		};

		// Show the dialog and return the selected folder path
		return dialog.ShowDialog() == CommonFileDialogResult.Ok
			? dialog.FileName
			: string.Empty;
	}
}
