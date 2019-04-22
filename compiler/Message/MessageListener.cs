using System;
namespace dogma.Message
{
    public interface MessageListener
    {
        void MessageReceived(Message message);
    }
}
