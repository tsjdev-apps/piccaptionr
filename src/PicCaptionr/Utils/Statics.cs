namespace PicCaptionr.Utils;

/// <summary>
/// Central static class for storing application-wide constants.
/// </summary>
public static class Statics
{
	// Hosts
	public const string OpenAiKey = "OpenAI";
	public const string AzureOpenAiKey = "Azure OpenAI";

	// OpenAI Model Keys
	public const string GPT51Key = "gpt-5.1";
	public const string GPT5Key = "gpt-5";
	public const string GPT5MiniKey = "gpt-5-mini";
	public const string GPT5NanoKey = "gpt-5-nano";
	public const string GPT41Key = "gpt-4.1";
	public const string GPT41MiniKey = "gpt-4.1-mini";
	public const string GPT41NanoKey = "gpt-4.1-nano";
	public const string GPT4oKey = "gpt-4o";
	public const string GPT4oMiniKey = "gpt-4o-mini";
	public const string DefaultDeploymentName = GPT4oMiniKey;

	// Supported Languages
	public const string EnglishLanguage = "English";
	public const string GermanLanguage = "German";
	public const string SpanishLanguage = "Spanish";
	public const string DefaultLanguage = EnglishLanguage;

	// Image Settings
	public const int ImageWidth = 640;
	public const int ImageHeight = 480;
}

