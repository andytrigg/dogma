namespace dogma.Message
{
    public class Message
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