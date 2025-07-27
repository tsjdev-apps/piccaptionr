namespace PicCaptionr.Services.Interfaces;

/// <summary>
/// Service interface for tracking and displaying process messages.
/// </summary>
public interface IProcessTrackerService
{
	/// <summary>
	/// Provides functionality to track and 
	/// display process messages.
	/// </summary>
	void TrackProcess(string message);
}
