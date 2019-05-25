namespace dogma.Message
{
    public interface IMessage
    {
    }

    public class Message : IMessage
    {
        public readonly object Body;
        public readonly MessageType Type;

        public Message(MessageType Type, object Body)
        {
            this.Type = Type;
            this.Body = Body;
        }
    }
}