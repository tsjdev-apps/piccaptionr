using PicCaptionr.Services.Interfaces;
using Spectre.Console;

namespace PicCaptionr.ConsoleApp.Services;

/// <summary>
/// Provides a mechanism to track and log the progress of processing steps.
/// </summary>
public class ProcessTrackerService : IProcessTrackerService
{
	/// <inheritdoc/>
	public void TrackProcess(string message)
	{
		AnsiConsole.Markup(
			$"[bold blue]:right_arrow:[/] [white]{message}[/]");
	}
}
