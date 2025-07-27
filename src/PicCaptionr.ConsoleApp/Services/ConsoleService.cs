using PicCaptionr.ConsoleApp.Services.Interfaces;
using Spectre.Console;

namespace PicCaptionr.ConsoleApp.Services;

/// <summary>
/// Provides methods for interacting with the console.
/// Implements <see cref="IConsoleService"/> to handle 
/// input/output operations in a console environment.
/// </summary>
public class ConsoleService : IConsoleService
{
	/// <inheritdoc/>
	public void ShowHeader()
	{
		AnsiConsole.Clear();

		var grid = new Grid();
		grid.AddColumn();

		grid.AddRow(
			new FigletText("PicCaptionr")
				.Centered()
				.Color(Color.Red));

		grid.AddRow(
			Align.Center(
				new Panel("[red]Sample by Thomas Sebastian Jensen " +
				"([link]https://www.tsjdev-apps.de[/])[/]")));

		AnsiConsole.Write(grid);
		AnsiConsole.WriteLine();
	}

	/// <inheritdoc/>
	public string SelectFromOptions(
		List<string> options,
		string prompt)
	{
		ShowHeader();

		return AnsiConsole.Prompt(
			new SelectionPrompt<string>()
				.Title(prompt)
				.AddChoices(options));
	}

	/// <inheritdoc/>
	public string? GetStringFromConsole(string prompt, bool allowEmptyValue = false, bool validateLength = true)
	{
		ShowHeader();

		var textPrompt = new TextPrompt<string?>(prompt)
			.PromptStyle("white")
			.ValidationErrorMessage("[red]Invalid input[/]")
			.Validate(value => ValidateInput(value, allowEmptyValue, validateLength));

		if (allowEmptyValue)
		{
			textPrompt = textPrompt.AllowEmpty();
		}

		return AnsiConsole.Prompt(textPrompt);
	}

	/// <inheritdoc/>
	public string GetUrlFromConsole(string prompt, bool validateLength = true)
	{
		ShowHeader();

		return AnsiConsole.Prompt(
			new TextPrompt<string>(prompt)
				.PromptStyle("white")
				.ValidationErrorMessage("[red]Invalid URL[/]")
				.Validate(value =>
				{
					var result = ValidateInput(value, false, validateLength);
					if (!result.Successful)
					{
						return result;
					}

					if (!Uri.TryCreate(value, UriKind.Absolute, out var uriResult) ||
						(uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
					{
						return ValidationResult.Error("[red]Invalid URL format[/]");
					}

					return ValidationResult.Success();
				}));
	}

	/// <inheritdoc/>
	public void WriteError(string errorMessage)
	{
		AnsiConsole.WriteLine();
		AnsiConsole.MarkupLine($"[red]{errorMessage}[/]");
	}

	/// <inheritdoc/>
	public void WriteMarkup(
		string markup)
	{
		AnsiConsole.MarkupLine(markup);
	}

	/// <inheritdoc/>
	public void WriteException(
		Exception ex)
	{
		AnsiConsole.WriteException(
			ex, 
			ExceptionFormats.ShortenEverything | ExceptionFormats.ShowLinks);
	}

	/// <inheritdoc/>
	public void WaitForExit()
	{
		AnsiConsole.MarkupLine("[grey]Press any key to exit...[/]");
		Console.ReadKey();
	}

	/// <summary>
	/// Validates a user-provided string based on length and emptiness rules.
	/// </summary>
	/// <param name="value">
	/// The input string to validate.</param>
	/// <param name="allowEmptyValue">
	/// Specifies whether empty or whitespace-only input is allowed.</param>
	/// <param name="validateLength">
	/// Specifies whether to enforce a maximum length of 200 characters.</param>
	/// <returns>
	/// A <see cref="ValidationResult"/> indicating whether the input is valid or contains an error message.
	/// </returns>
	static ValidationResult ValidateInput(string? value, bool allowEmptyValue, bool validateLength)
	{
		if (!allowEmptyValue && (string.IsNullOrWhiteSpace(value) || value.Length < 3))
		{
			return ValidationResult.Error("[red]Value too short[/]");
		}

		return validateLength && value?.Length > 200
			? ValidationResult.Error("[red]Value too long[/]")
			: ValidationResult.Success();
	}
}
