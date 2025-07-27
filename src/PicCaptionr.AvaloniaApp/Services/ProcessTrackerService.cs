using PicCaptionr.Services.Interfaces;

namespace PicCaptionr.AvaloniaApp.Services;

/// <summary>
/// Provides a service to log process-related messages 
/// to the activity log. Ensures thread-safe updates 
/// by using the current <see cref="SynchronizationContext"/>.
/// </summary>
/// <param name="activityLogService">
/// The service responsible for storing activity log entries.</param>

public class ProcessTrackerService(
	IActivityLogService activityLogService) : IProcessTrackerService
{
	// Captures the synchronization context (usually the UI thread)
	// at the time of construction
	readonly SynchronizationContext? syncContext = SynchronizationContext.Current;

	/// <inheritdoc/>
	public void TrackProcess(string message)
	{
		// Gets the current synchronization context
		if (syncContext != null)
		{
			// Posts the message to the synchronization context
			syncContext.Post(_ =>
			{
				activityLogService.AddEntry(message.Trim());
			}, null);
		}
		else
		{
			// Fallback – runs directly if no UI context is available
			activityLogService.AddEntry(message.Trim());
		}
	}
}
