using System.Windows;
using System.Windows.Threading;
using PicCaptionr.Services.Interfaces;

namespace PicCaptionr.WPFApp.Services;

/// <summary>
/// WPF-specific implementation of <see cref="IProcessTrackerService"/>.
/// Ensures that process messages are safely added 
/// to the activity log on the UI thread.
/// </summary>
public class ProcessTrackerService(
	IActivityLogService activityLogService) : IProcessTrackerService
{
	/// <inheritdoc/>
	public void TrackProcess(string message)
	{
		// Ensure the Application and Dispatcher are available
		if (Application.Current?.Dispatcher is not Dispatcher dispatcher)
		{
			return;
		}

		// Use the UI dispatcher to update the log in a thread-safe manner
		dispatcher.InvokeAsync(() =>
		{
			activityLogService.AddEntry(message.Trim());
		});
	}
}
