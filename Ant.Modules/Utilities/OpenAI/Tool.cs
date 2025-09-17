using OpenAI.Chat;

namespace ShareInvest.Utilities.OpenAI;

public static class Tool
{
    public static ChatTool GetDailyChartCandlesticks(byte[] bytes, string description)
    {
        return ChatTool.CreateFunctionTool(nameof(GetDailyChartCandlesticks), functionParameters: new BinaryData(bytes), functionDescription: description);
    }

    public static ChatTool GetStockItem(byte[] bytes, string description)
    {
        return ChatTool.CreateFunctionTool(nameof(GetStockItem), functionParameters: new BinaryData(bytes), functionDescription: description);
    }

    public static ChatTool GetStockItems(byte[] bytes, string description)
    {
        return ChatTool.CreateFunctionTool(nameof(GetStockItems), functionParameters: new BinaryData(bytes), functionDescription: description);
    }

    public static ChatTool WebSearch(byte[] bytes, string description)
    {
        return ChatTool.CreateFunctionTool(nameof(WebSearch), functionParameters: new BinaryData(bytes), functionDescription: description);
    }
}