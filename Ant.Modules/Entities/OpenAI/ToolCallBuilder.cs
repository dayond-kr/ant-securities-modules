using System.Buffers;
using System.Text;

namespace ShareInvest.Entities.OpenAI;

public record ToolCallBuilder
{
    public string? Id
    {
        get; set;
    }

    public string? Name
    {
        get; set;
    }

    public ArrayBufferWriter<byte> ArgBuffer
    {
        get;
    }
        = new(0x400);

    public void AppendArgs(BinaryData? delta, string? deltaString = null)
    {
        if (delta is not null)
        {
            var mem = delta.ToMemory();

            if (mem.Length > 0)
            {
                ArgBuffer.Write(mem.Span);
            }
            return;
        }

        if (string.IsNullOrEmpty(deltaString) is false)
        {
            var bytes = Encoding.UTF8.GetBytes(deltaString);

            ArgBuffer.Write(bytes);
        }
    }

    public BinaryData GetArgsBinaryData() => new(ArgBuffer.WrittenMemory);
}