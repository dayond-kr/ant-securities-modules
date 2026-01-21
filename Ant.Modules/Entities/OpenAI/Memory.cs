using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ShareInvest.Entities.OpenAI;

[JsonConverter(typeof(StringEnumConverter))]
public enum MemoryType
{
    [EnumMember(Value = "profile")]
    Profile,
    [EnumMember(Value = "preference")]
    Preference,
    [EnumMember(Value = "task_state")]
    TaskState,
    [EnumMember(Value = "long_term")]
    LongTermFact,
    [EnumMember(Value = "summary")]
    SessionSummary,
    [EnumMember(Value = "safety")]
    SafetyRule
}

public class Memory
{
    [Key]
    public long Id
    {
        get; set;
    }

    [StringLength(0x40), Required]
    public string? UserId
    {
        get; set;
    }

    public MemoryType Type
    {
        get; set;
    }

    [StringLength(0x100), Required]
    public string? Subject
    {
        get; set;
    }

    [StringLength(0x400), Required]
    public string? Value
    {
        get; set;
    }

    public double Confidence
    {
        get; set;
    }

    public int Importance
    {
        get; set;
    }

    public DateTime CreatedAt
    {
        get; set;
    }

    public DateTime LastSeen
    {
        get; set;
    }

    public DateTime ExpiresAt
    {
        get; set;
    }

    public int SeenCount
    {
        get; set;
    }

    [NotMapped, JsonProperty("ttlDays")]
    public int TTL
    {
        get; set;
    }

    public string? ConsentScope
    {
        get; set;
    }

    public string? Sensitivity
    {
        get; set;
    }

    [NotMapped]
    public string? Action
    {
        get; set;
    }

    public static double Score(Memory item, DateTime now)
    {
        var days = Math.Max(0, (now - item.LastSeen).TotalDays);
        var recency = Math.Pow(0.5, days / 14.0);

        return recency * (1 + Math.Log10(1 + item.SeenCount)) * (item.Importance / 3.0);
    }

    public static DateTime GetDefaultExpiry(MemoryType t) => t switch
    {
        MemoryType.Profile => DateTime.UtcNow.AddYears(0x100),
        MemoryType.Preference => DateTime.UtcNow.AddDays(180),
        MemoryType.TaskState => DateTime.UtcNow.AddDays(14),
        MemoryType.SessionSummary => DateTime.UtcNow.AddDays(7),
        MemoryType.LongTermFact => DateTime.UtcNow.AddDays(365),
        MemoryType.SafetyRule => DateTime.UtcNow.AddDays(365),
        _ => DateTime.UtcNow.AddDays(90)
    };
}

public sealed class MemoryPayload
{
    public Memory[]? Memories
    {
        get; set;
    }
}