using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using PicCaptionr.AvaloniaApp.Services;
using PicCaptionr.AvaloniaApp.Services.Interfaces;
using PicCaptionr.AvaloniaApp.ViewModels;
using PicCaptionr.AvaloniaApp.Views;
using PicCaptionr.Services;
using PicCaptionr.Services.Interfaces;

namespace PicCaptionr.AvaloniaApp;

/// <summary>
/// The entry point of the Avalonia application. 
/// Sets up dependency injection, disables Avalonia's 
/// built-in validation to avoid conflicts, 
/// and wires the main window.
/// </summary>
public partial class App : Application
{
	/// <summary>
	/// The application's service provider for resolving dependencies.
	/// </summary>
	public static IServiceProvider? Services { get; private set; }

	/// <summary>
	/// Initializes the Avalonia application and loads XAML resources.
	/// </summary>
	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
	}

	/// <summary>
	/// Called when the framework initialization is completed.
	/// Responsible for setting up services, 
	/// injecting the MainWindowViewModel, and attaching the 
	/// main window to the application lifetime.
	/// </summary>
	public override void OnFrameworkInitializationCompleted()
	{
		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			// Prevent Avalonia from running its own DataAnnotations
			// validation in parallel
			DisableAvaloniaDataAnnotationValidation();

			// Create and configure the DI container
			ServiceCollection serviceCollection = new();
			RegisterServices(serviceCollection);
			Services = serviceCollection.BuildServiceProvider();

			// Instantiate and bind the main window
			MainWindow mainWindow = new()
			{
				DataContext = Services.GetRequiredService<MainWindowViewModel>()
			};

			// Provide the window to services that require it
			// (e.g., FilePickers)
			ConfigureServices(mainWindow);

			// Set the window as the main window of the app
			desktop.MainWindow = mainWindow;
		}

		base.OnFrameworkInitializationCompleted();
	}

	/// <summary>
	/// Disables Avalonia’s built-in data annotation 
	/// validation to avoid conflicts with custom validation logic.
	/// </summary>
	static void DisableAvaloniaDataAnnotationValidation()
	{
		// Get an array of plugins to remove
		var dataValidationPluginsToRemove =
			BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

		// remove each entry found
		foreach (DataAnnotationsValidationPlugin? plugin in dataValidationPluginsToRemove)
		{
			BindingPlugins.DataValidators.Remove(plugin);
		}
	}


	/// <summary>
	/// Registers all application services and 
	/// view models into the DI container.
	/// </summary>
	/// <param name="services">
	/// The service collection to register dependencies into.</param>
	static void RegisterServices(IServiceCollection services)
	{
		// Shared Services
		services.AddSingleton<IActivityLogService, ActivityLogService>();
		services.AddSingleton<IChatService, ChatService>();
		services.AddSingleton<IImageService, ImageService>();
		services.AddSingleton<IPromptService, PromptService>();
		services.AddSingleton<IStorageService, StorageService>();

		// Avalonia UI-specific Services		
		services.AddSingleton<IFolderPickerService, FolderPickerService>();
		services.AddSingleton<IProcessTrackerService, ProcessTrackerService>();
		services.AddSingleton<IWindowService, WindowService>();

		// ViewModels
		services.AddSingleton<MainWindowViewModel>();
	}

	/// <summary>
	/// Passes the main window to services that need access to it.
	/// </summary>
	/// <param name="mainWindow">
	/// The root window of the application.</param>
	static void ConfigureServices(Window mainWindow)
	{
		if (Services is null)
		{
			return;
		}

		IWindowService windowService = Services.GetRequiredService<IWindowService>();
		windowService.SetMainWindow(mainWindow);
	}
}