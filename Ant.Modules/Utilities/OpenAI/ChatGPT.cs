using OpenAI;
using OpenAI.Chat;

using System.ClientModel;

namespace ShareInvest.Utilities.OpenAI;

public class ChatGPT : OpenAIClient
{
    public ChatGPT(string key, string content, string router, string memory, OpenAIClientOptions clientOptions, ChatCompletionOptions completionOptions) : base(new ApiKeyCredential(key), clientOptions)
    {
        Options = completionOptions;

        SystemChatMessage = new SystemChatMessage(content);
        ModelRouter = new SystemChatMessage(router);
        Memory = new SystemChatMessage(memory);
    }

    public ChatGPT(string key, string content, string router, string memory, ChatCompletionOptions options) : base(key)
    {
        Options = options;

        SystemChatMessage = new SystemChatMessage(content);
        ModelRouter = new SystemChatMessage(router);
        Memory = new SystemChatMessage(memory);
    }

    public SystemChatMessage SystemChatMessage
    {
        get;
    }

    public SystemChatMessage Memory
    {
        get;
    }

    public SystemChatMessage ModelRouter
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
            EndUserId = userId,
        };

        foreach (var tool in Options.Tools)
        {
            options.Tools.Add(tool);
        }
        return options;
    }
}