using System;
using dogma.Intermediate;
using dogma.Message;

namespace dogma.Backend
{
    public abstract class Backend: MessageProducer
    {
        private readonly MessageHandler messageHandler = new MessageHandler();

        public void AddMessageListener(MessageListener listener)
        {
            messageHandler.AddListener(listener);
        }

        public abstract void Process(IntermediateCode iCode, SymbolTable symTab);

        public void RemoveMessageListener(MessageListener listener)
        {
            messageHandler.RemoveListener(listener);
        }

        public void SendMessage(Message.Message message)
        {
            messageHandler.SendMessage(message);
        }
    }
}
