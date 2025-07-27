using PicCaptionr.Models;

namespace PicCaptionr.Services.Interfaces;

/// <summary>
/// Represents a service for interacting with AI chat clients.
/// </summary>
public interface IChatService
{
	/// <summary>
	///	Initializes the Azure OpenAI chat client 
	///	with the specified parameters.
	/// </summary>
	/// <param name="azureOpenAIEndpoint">
	/// The Azure OpenAI endpoint.</param>
	/// <param name="azureOpenAIKey">
	/// The Azure OpenAI key.</param>
	/// <param name="azureOpenAIDeploymentName">
	/// The Azure OpenAI model deployment name.</param>
	void InitAzureOpenAIChatClient(
		string azureOpenAIEndpoint, 
		string azureOpenAIKey, 
		string azureOpenAIDeploymentName);

	/// <summary>
	///	Initializes the OpenAI chat client
	///	with the specified parameters.
	/// </summary>
	/// <param name="openAIKey">
	/// The OpenAI key.</param>
	/// <param name="openAIDeploymentName">
	/// The OpenAI model deployment name.</param>
	void InitOpenAIChatClient(
		string openAIKey, 
		string openAIDeploymentName);

	/// <summary>
	///	Makes a vision request to the AI service asynchronously.
	/// </summary>
	/// <param name="image">
	///	The image as byte array.</param>
	/// <param name="fileExtension">
	/// The file extension of the image.</param>
	/// <param name="prompt">
	/// The prompt for the vision request.</param>
	/// <returns>
	/// A task representing the operation that returns an <see cref="AIResponse"/>.
	/// </returns>
	Task<AIResponse> MakeVisionRequestAsync(
		byte[] image, 
		string fileExtension, 
		string prompt);
}