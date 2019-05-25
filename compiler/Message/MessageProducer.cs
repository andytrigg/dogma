namespace dogma.Message
{
    public interface IMessageProducer
    {
        void AddMessageListener(MessageListener listener);

        void RemoveMessageListener(MessageListener listener);

        void SendMessage(IMessage message);
    }
}