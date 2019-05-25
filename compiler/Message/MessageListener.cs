namespace dogma.Message
{
    public interface MessageListener
    {
        void MessageReceived(IMessage message);
    }
}