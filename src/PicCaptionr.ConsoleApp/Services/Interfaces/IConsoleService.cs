namespace PicCaptionr.ConsoleApp.Services.Interfaces;

/// <summary>
/// Interface for console services.
/// </summary>
public interface IConsoleService
{
	/// <summary>
	/// Prompts the user to enter a string via the console.
	/// </summary>
	/// <param name="prompt">
	/// The message displayed to the user as a prompt.</param>
	/// <param name="allowEmptyValue">
	/// Specifies whether an empty input is considered valid.</param>
	/// <param name="validateLength">
	/// Specifies whether the input should be validated against a maximum length constraint (default: 200 characters).</param>
	/// <returns>
	/// The user input as a string, or <c>null</c> if no value was entered and empty input is allowed.
	/// </returns>
	string? GetStringFromConsole(
		string prompt,
		bool allowEmptyValue = false,
		bool validateLength = true);

	/// <summary>
	///	Gets a URL input from the console.
	/// </summary>
	/// <param name="prompt">
	/// The prompt message to display.</param>
	/// <param name="validateLength">
	/// Indicates whether to validate the length of the input.</param>
	/// <returns>
	/// The string input from the console.
	/// </returns>
	string GetUrlFromConsole(string prompt, bool validateLength = true);

	/// <summary>
	///	Selects an option from a list of options.
	/// </summary>
	/// <param name="options">
	/// The list of options to choose from.</param>
	/// <param name="prompt">
	/// The prompt message to display.</param>
	/// <returns>
	/// The selected option.
	/// </returns>
	string SelectFromOptions(List<string> options, string prompt);

	/// <summary>
	///	Shows the header.
	/// </summary>
	void ShowHeader();

	/// <summary>
	///	Writes an error message to the console.
	/// </summary>
	/// <param name="errorMessage">
	/// The error message to write.</param>
	void WriteError(string errorMessage);

	/// <summary>
	/// Writes a markup string to the console.
	/// </summary>
	/// <param name="markup">
	/// The message to write.</param>
	void WriteMarkup(string markup);

	/// <summary>
	/// Writes an exception message to the console.
	/// </summary>
	/// <param name="ex">
	/// The error to write.</param>
	void WriteException(Exception ex);

	/// <summary>
	/// Waits for the console application to exit.
	/// </summary>
	void WaitForExit();
}