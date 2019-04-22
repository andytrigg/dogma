using System;
using System.Collections.Generic;

namespace dogma.Message
{
    public class MessageHandler
    {
        private List<MessageListener> messageListeners;

        public MessageHandler() => messageListeners = new List<MessageListener>();

        public void AddListener(MessageListener listener)
        {
            messageListeners.Add(listener);
        }

        public void RemoveListener(MessageListener listener)
        {
            messageListeners.Remove(listener);
        }

        public void SendMessage(Message message)
        {
            foreach (MessageListener listener in messageListeners)
            {
                listener.MessageReceived(message);
            }
        }
    }
}
