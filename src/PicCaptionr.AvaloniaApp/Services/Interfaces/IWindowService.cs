using Avalonia.Controls;

namespace PicCaptionr.AvaloniaApp.Services.Interfaces;

/// <summary>
/// Provides functionality to manage and access the 
/// main application window.
/// This is useful for services that require access to 
/// the current <see cref="Window"/>, such as file pickers 
/// or dialog presenters.
/// </summary>
public interface IWindowService
{
	/// <summary>
	/// Returns the currently registered 
	/// main application window.
	/// </summary>
	/// <returns>
	/// The current <see cref="Window"/> instance.
	/// </returns>
	Window GetCurrentWindow();

	/// <summary>
	/// Registers the given window as the 
	/// main application window.
	/// </summary>
	/// <param name="window">
	/// The <see cref="Window"/> instance to register.</param>
	void SetMainWindow(Window window);
}