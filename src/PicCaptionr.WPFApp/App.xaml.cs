using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PicCaptionr.Services;
using PicCaptionr.Services.Interfaces;
using PicCaptionr.WPFApp.Services;
using PicCaptionr.WPFApp.ViewModels;

namespace PicCaptionr.WPFApp;

/// <summary>
/// The main entry point for the WPF application. 
/// Responsible for bootstrapping dependency injection 
/// and launching the main window.
/// </summary>
public partial class App : Application
{
	/// <summary>
	/// Gets the global service provider for resolving 
	/// dependencies throughout the application.
	/// </summary>
	public static IServiceProvider? ServiceProvider { get; private set; }


	/// <summary>
	/// Called when the application starts.
	/// Sets up the service collection, registers dependencies, 
	/// and launches the main window.
	/// </summary>
	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		// Register services using Microsoft's dependency injection
		ServiceProvider = new ServiceCollection()

			// Core shared services
			.AddSingleton<IActivityLogService, ActivityLogService>()
			.AddSingleton<IChatService, ChatService>()
			.AddSingleton<IImageService, ImageService>()
			.AddSingleton<IPromptService, PromptService>()
			.AddSingleton<IStorageService, StorageService>()

			// WPF-specific services
			.AddSingleton<IFolderPickerService, FolderPickerService>()
			.AddSingleton<IProcessTrackerService, ProcessTrackerService>()

			// UI-related registrations
			.AddSingleton<MainWindowViewModel>()
			.AddSingleton<MainWindow>()

			// Build the service provider
			.BuildServiceProvider();

		// Retrieve and show the main window
		MainWindow mainWindow
			= ServiceProvider.GetRequiredService<MainWindow>();
		mainWindow.Show();
	}
}

