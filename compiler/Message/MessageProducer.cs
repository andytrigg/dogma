using System;
namespace dogma.Message
{
    public interface MessageProducer
    {
        void AddMessageListener(MessageListener listener);

        void RemoveMessageListener(MessageListener listener);

        void SendMessage(Message message);
    }
}
