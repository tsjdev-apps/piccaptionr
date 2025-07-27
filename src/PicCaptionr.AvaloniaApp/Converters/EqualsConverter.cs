using System.Globalization;
using Avalonia.Data.Converters;

namespace PicCaptionr.AvaloniaApp.Converters;

/// <summary>
/// A value converter used for equality comparison in data bindings.
/// Typically used in XAML to show or hide UI elements based on whether
/// a bound value equals a specific parameter.
/// </summary>
public class EqualsConverter : IValueConverter
{
	/// <summary>
	/// A shared singleton instance of the converter to use in XAML.
	/// </summary>
	public static readonly EqualsConverter Instance = new();

	/// <summary>
	/// Converts the input value by comparing it to the given parameter.
	/// Returns true if both are equal, otherwise false.
	/// </summary>
	/// <param name="value">
	/// The source value passed from the binding.</param>
	/// <param name="targetType">
	/// The expected type (not used here).</param>
	/// <param name="parameter">
	/// The value to compare to.</param>
	/// <param name="culture">
	/// The current culture (not used).</param>
	/// <returns>
	/// True if the value equals the parameter; otherwise, false.
	/// </returns>
	public object? Convert(
		object? value, 
		Type targetType, 
		object? parameter, 
		CultureInfo culture)
	{
		return Equals(value, parameter);
	}

	/// <summary>
	/// This converter does not support two-way binding.
	/// Always throws <see cref="NotImplementedException"/> because equality comparison
	/// is not reversible in this context.
	/// </summary>
	public object? ConvertBack(
		object? value, 
		Type targetType, 
		object? parameter, 
		CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
