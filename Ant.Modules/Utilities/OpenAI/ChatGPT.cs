using OpenAI;
using OpenAI.Chat;

using System.ClientModel;

namespace ShareInvest.Utilities.OpenAI;

public class ChatGPT : ChatClient
{
    public ChatGPT(string key, string content, OpenAIClientOptions clientOptions, ChatCompletionOptions completionOptions) : base(model, new ApiKeyCredential(key), clientOptions)
    {
        Options = completionOptions;
        SystemChatMessage = new SystemChatMessage(content);
    }

    public ChatGPT(string key, string content, ChatCompletionOptions options) : base(model, key)
    {
        Options = options;
        SystemChatMessage = new SystemChatMessage(content);
    }

    public SystemChatMessage SystemChatMessage
    {
        get;
    }

    public ChatCompletionOptions Options
    {
        get;
    }

    public ChatCompletionOptions CreateOptions(string userId)
    {
        var options = new ChatCompletionOptions
        {
            ToolChoice = Options.ToolChoice,
            EndUserId = userId
        };

        foreach (var tool in Options.Tools)
        {
            options.Tools.Add(tool);
        }
        return options;
    }

    const string model = "gpt-5";
}