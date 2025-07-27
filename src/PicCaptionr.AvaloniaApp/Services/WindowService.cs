using Avalonia.Controls;
using PicCaptionr.AvaloniaApp.Services.Interfaces;

namespace PicCaptionr.AvaloniaApp.Services;

/// <summary>
/// Provides access to the application's main window.
/// Useful for services that require a reference 
/// to the active <see cref="Window"/> instance 
/// (e.g. file pickers or dialogs).
/// </summary>
public class WindowService : IWindowService
{
	Window? mainWindow;

	/// <inheritdoc/>
	public void SetMainWindow(Window window)
		=> mainWindow = window;

	/// <inheritdoc/>
	public Window GetCurrentWindow()
		=> mainWindow 
			?? throw new InvalidOperationException(
				"Window not set.");
}
