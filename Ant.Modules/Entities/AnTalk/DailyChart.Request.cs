namespace ShareInvest.Entities.AnTalk;

public struct RequestDailyChart
{
    public string? Date
    {
        get; set;
    }

    public string Code
    {
        get; set;
    }

    public int Period
    {
        get; set;
    }
}