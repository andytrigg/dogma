using dogma.Intermediate;
using dogma.Message;

namespace dogma.Backend
{
    public abstract class Backend : IMessageProducer
    {
        private readonly IMessageHandler _messageHandler;

        protected Backend(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        public void AddMessageListener(IMessageListener listener)
        {
            _messageHandler.AddListener(listener);
        }

        public void RemoveMessageListener(IMessageListener listener)
        {
            _messageHandler.RemoveListener(listener);
        }

        public void SendMessage(IMessage message)
        {
            _messageHandler.SendMessage(message);
        }

        public abstract void Process(IntermediateCode iCode, SymbolTable symTab);
    }
}