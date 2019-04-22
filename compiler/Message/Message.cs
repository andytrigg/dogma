using System;
namespace dogma.Message
{
    public class Message
    {
        public readonly MessageType Type;
        public readonly object Body;

        public Message(MessageType Type, Object Body)
        {
            this.Type = Type;
            this.Body = Body;
        }
    }
}
