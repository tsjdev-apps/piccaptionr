using System.ClientModel;
using Azure.AI.OpenAI;
using OpenAI;
using OpenAI.Chat;
using PicCaptionr.Models;
using PicCaptionr.Services.Interfaces;
using PicCaptionr.Utils;

namespace PicCaptionr.Services;

/// <summary>
/// Provides functionality for handling chat 
/// interactions with an AI model.
/// </summary>
public class ChatService : IChatService
{
	// Holds the chat client instance,
	// which can be initialized using Azure OpenAI or OpenAI directly.
	ChatClient? chatClient;

	/// <inheritdoc/>
	public void InitAzureOpenAIChatClient(
		string azureOpenAIEndpoint,
		string azureOpenAIKey,
		string azureOpenAIDeploymentName)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(azureOpenAIEndpoint);
		ArgumentException.ThrowIfNullOrWhiteSpace(azureOpenAIKey);
		ArgumentException.ThrowIfNullOrWhiteSpace(azureOpenAIDeploymentName);

		// Initializes the chat client using Azure OpenAI endpoint,
		// key, and deployment name
		chatClient =
			new AzureOpenAIClient(
				new Uri(azureOpenAIEndpoint),
				new ApiKeyCredential(azureOpenAIKey))
			.GetChatClient(azureOpenAIDeploymentName);
	}

	/// <inheritdoc/>
	public void InitOpenAIChatClient(
		string openAIKey,
		string openAIDeploymentName)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(openAIKey);
		ArgumentException.ThrowIfNullOrWhiteSpace(openAIDeploymentName);

		// Initializes the chat client using OpenAI API key
		// and deployment name
		chatClient =
			new OpenAIClient(
				new ApiKeyCredential(openAIKey))
			.GetChatClient(openAIDeploymentName);
	}

	/// <inheritdoc/>
	public async Task<AIResponse> MakeVisionRequestAsync(
		byte[] image,
		string fileExtension,
		string prompt)
	{
		if (chatClient is null)
		{
			throw new InvalidOperationException(
				"Chat client is not initialized. " +
				"Please call one of the Init methods first.");
		}

		if (image is null || image.Length == 0)
		{
			throw new ArgumentException(
				"Image data must not be null or empty.",
				nameof(image));
		}

		if (string.IsNullOrWhiteSpace(fileExtension))
		{
			throw new ArgumentException(
				"File extension must not be null or whitespace.",
				nameof(fileExtension));
		}

		if (string.IsNullOrWhiteSpace(prompt))
		{
			throw new ArgumentException(
				"Prompt must not be null or whitespace.",
				nameof(prompt));
		}

		// Configure the chat completion options
		ChatCompletionOptions chatCompletionsOptions = new()
		{
			MaxOutputTokenCount = 5000,
			Temperature = 0.7f
		};

		// Build the message list with prompt and image
		List<ChatMessage> chatMessages = [
			new UserChatMessage(prompt),
			new UserChatMessage([
				ChatMessageContentPart.CreateImagePart(
					BinaryData.FromBytes(image),
					$"image/{fileExtension}",
					ChatImageDetailLevel.Low)
			])
		];

		// Make the chat completion request
		ClientResult<ChatCompletion> result
			= await chatClient
				.CompleteChatAsync(chatMessages, chatCompletionsOptions)
				.ConfigureAwait(false);

		// Extract token usage and build response
		ChatTokenUsage usage = result.Value.Usage;

		return new AIResponse(
			result.Value.Content[0].Text,
			ContentSanitizer.CleanContent(result.Value.Content[0].Text),
			usage.InputTokenCount,
			usage.OutputTokenCount);
	}
}
