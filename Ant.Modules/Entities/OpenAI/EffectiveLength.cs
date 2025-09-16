using Newtonsoft.Json;

using System.Runtime.Serialization;

namespace ShareInvest.Entities.OpenAI;

public struct EffectiveLength
{
    public string Route
    {
        get; set;
    }

    public double Confidence
    {
        get; set;
    }

    [DataMember, JsonProperty("effTokens")]
    public int Tokens
    {
        get; set;
    }
}