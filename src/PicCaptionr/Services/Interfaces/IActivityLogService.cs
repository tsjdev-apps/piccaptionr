using System.Collections.ObjectModel;

namespace PicCaptionr.Services.Interfaces;

/// <summary>
/// Provides a logging mechanism to store 
/// and retrieve activity log messages.
/// </summary>
public interface IActivityLogService
{
	/// <summary>
	/// Adds a new message to the activity log.
	/// </summary>
	/// <param name="message">
	/// The message to add to the log.</param>
	void AddEntry(string message);

	/// <summary>
	/// Retrieves all logged messages as 
	/// an observable collection.
	/// </summary>
	/// <returns>
	/// An ObservableCollection containing log messages.</returns>
	ObservableCollection<string> GetEntries();
}