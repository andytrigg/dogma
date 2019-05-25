using System.Collections.Generic;

namespace dogma.Message
{
    public interface IMessageHandler
    {
        void AddListener(IMessageListener listener);
        void RemoveListener(IMessageListener listener);
        void SendMessage(IMessage message);
    }

    public class MessageHandler : IMessageHandler
    {
        private readonly List<IMessageListener> _messageListeners;

        public MessageHandler()
        {
            _messageListeners = new List<IMessageListener>();
        }

        public void AddListener(IMessageListener listener)
        {
            _messageListeners.Add(listener);
        }

        public void RemoveListener(IMessageListener listener)
        {
            _messageListeners.Remove(listener);
        }

        public void SendMessage(IMessage message)
        {
            foreach (var listener in _messageListeners) listener.MessageReceived(message);
        }
    }
}