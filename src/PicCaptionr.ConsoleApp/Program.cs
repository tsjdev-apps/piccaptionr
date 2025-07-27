using Microsoft.Extensions.DependencyInjection;
using PicCaptionr.ConsoleApp.Services;
using PicCaptionr.ConsoleApp.Services.Interfaces;
using PicCaptionr.Services;
using PicCaptionr.Services.Interfaces;


// Set up the service provider and register all required dependencies
using ServiceProvider serviceProvider =

	new ServiceCollection()

		// Console-specific services
		.AddSingleton<IConsoleService, ConsoleService>()
		.AddSingleton<IProcessTrackerService, ProcessTrackerService>()

		// Core business logic services
		.AddSingleton<IChatService, ChatService>()
		.AddSingleton<IImageService, ImageService>()
		.AddSingleton<IPromptService, PromptService>()
		.AddSingleton<IStorageService, StorageService>()

		// Main process orchestrator
		.AddSingleton<IProcessService, ProcessService>()

		// Build the dependency injection container
		.BuildServiceProvider();

// Resolve required services
IConsoleService consoleService
	= serviceProvider.GetRequiredService<IConsoleService>();
IProcessService processService
	= serviceProvider.GetRequiredService<IProcessService>();

try
{
	// Start the main image processing workflow
	await processService.StartProcessAsync()
		.ConfigureAwait(false);

	// Display a success message to the user
	consoleService.WriteMarkup(
		"[green]Program finished successfully.[/]");
}
catch (Exception ex)
{
	// Handle and display any errors that occurred during processing
	consoleService.WriteException(ex);
}
finally
{
	// Wait for user input before exiting
	consoleService.WaitForExit();
}