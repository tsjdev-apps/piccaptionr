namespace PicCaptionr.ConsoleApp.Services.Interfaces;

/// <summary>
/// Interface for process services.
/// </summary>
public interface IProcessService
{
	/// <summary>
	///	Starts the process asynchronously.
	/// </summary>
	/// <returns>
	/// A task representing the asynchronous operation.
	/// </returns>
	Task StartProcessAsync();
}