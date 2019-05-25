namespace dogma.Message
{
    public interface IMessageListener
    {
        void MessageReceived(IMessage message);
    }
}