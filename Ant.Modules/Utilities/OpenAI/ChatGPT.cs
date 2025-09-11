using OpenAI;
using OpenAI.Chat;

using System.ClientModel;

namespace ShareInvest.Utilities.OpenAI;

public class ChatGPT : ChatClient
{
    public ChatGPT(string key, OpenAIClientOptions clientOptions, ChatCompletionOptions completionOptions) : base(model, new ApiKeyCredential(key), clientOptions)
    {
        Options = completionOptions;
    }

    public ChatGPT(string key, ChatCompletionOptions options) : base(model, key)
    {
        Options = options;
    }

    public ChatCompletionOptions Options
    {
        get;
    }

    const string model = "gpt-5";
}