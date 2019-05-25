namespace dogma.Message
{
    public interface IMessageListener
    {
        void MessageReceived(Message message);
    }
}