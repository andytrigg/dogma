namespace dogma.Message
{
    public interface IMessageProducer
    {
        void AddMessageListener(IMessageListener listener);

        void RemoveMessageListener(IMessageListener listener);

        void SendMessage(IMessage message);
    }
}