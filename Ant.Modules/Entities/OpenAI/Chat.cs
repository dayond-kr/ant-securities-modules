using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations;

namespace ShareInvest.Entities.OpenAI;

public class Chat : Tracker
{
    [Key, JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
    public DateTime CreatedAt
    {
        get; set;
    }

    [Key, StringLength(0x40), JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
    public string? UserId
    {
        get; set;
    }

    [Required]
    public string? Message
    {
        get; set;
    }

    public bool Myself
    {
        get; set;
    }
}