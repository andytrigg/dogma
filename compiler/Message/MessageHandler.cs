using System.Collections.Generic;

namespace dogma.Message
{
    public interface IMessageHandler
    {
        void AddListener(MessageListener listener);
        void RemoveListener(MessageListener listener);
        void SendMessage(IMessage message);
    }

    public class MessageHandler : IMessageHandler
    {
        private readonly List<MessageListener> messageListeners;

        public MessageHandler()
        {
            messageListeners = new List<MessageListener>();
        }

        public void AddListener(MessageListener listener)
        {
            messageListeners.Add(listener);
        }

        public void RemoveListener(MessageListener listener)
        {
            messageListeners.Remove(listener);
        }

        public void SendMessage(IMessage message)
        {
            foreach (var listener in messageListeners) listener.MessageReceived(message);
        }
    }
}