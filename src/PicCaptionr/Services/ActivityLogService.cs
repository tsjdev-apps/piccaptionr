using System.Collections.ObjectModel;
using PicCaptionr.Services.Interfaces;

namespace PicCaptionr.Services;

/// <summary>
/// Provides a simple in-memory activity log service 
/// that stores and exposes log messages.
/// </summary>
public class ActivityLogService : IActivityLogService
{
	// Internal collection to store log entries.
	readonly ObservableCollection<string> entries = [];

	/// <inheritdoc/>
	public void AddEntry(string message)
	{
		entries.Add(message);
	}

	/// <inheritdoc/>
	public ObservableCollection<string> GetEntries()
		=> entries;
}
