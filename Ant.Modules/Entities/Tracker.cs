using System.ComponentModel.DataAnnotations.Schema;

namespace ShareInvest.Entities;

public class Tracker
{
    [NotMapped]
    public long Ticks
    {
        set
        {
            ticks = value;
        }
        get => (ticks - Cache.Epoch) / 10;
    }

    public static DateTime Convert(long timestamp)
    {
        return new DateTime(timestamp * 10 + Cache.Epoch, DateTimeKind.Utc);
    }

    long ticks;
}