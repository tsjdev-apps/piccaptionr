namespace PicCaptionr.Utils;

/// <summary>
/// Provides utility methods for cleaning 
/// and formatting AI-generated content.
/// </summary>
public static class ContentSanitizer
{
	/// <summary>
	/// Cleans up a string containing AI-generated content.
	/// This method removes unnecessary or escaped characters and 
	/// prepares the content for output.
	/// </summary>
	/// <param name="content">
	/// The original content string (may contain escaped characters or quotes).</param>
	/// <returns>
	/// A cleaned and properly formatted version of the input string.
	/// </returns>
	public static string CleanContent(string? content)
	{
		if (string.IsNullOrWhiteSpace(content))
		{
			return string.Empty;
		}

		return content
			// Remove leading and trailing double quotes
			// (sometimes added during JSON serialization)
			.Replace("\"", "")
			// Replace escaped newline characters with actual line breaks
			.Replace("\\n", "\n")
			// Normalize Windows line endings
			.Replace("\r\n", Environment.NewLine)
			// Ensure double line breaks are formatted properly
			.Replace("\n\n", Environment.NewLine + Environment.NewLine)
			// Trim leading/trailing whitespace
			.Trim();
	}
}
