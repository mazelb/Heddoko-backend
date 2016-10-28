
namespace Heddoko.Models.Streaming
{
    public class StreamMessage
    {
        public StreamMessageType MessageType { get; set; }

        public byte[] Message { get; set; }
    }
}