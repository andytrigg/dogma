using dogma.Intermediate;
using dogma.Message;

namespace dogma.Frontend
{
    public abstract class Parser : IMessageProducer
    {
        private readonly IMessageHandler _messageHandler;
        private IntermediateCode _intermediateCode;

        protected Parser(IScanner scanner, IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
            Scanner = scanner;
            _intermediateCode = null;
        }

        protected IScanner Scanner { get; }

        public void AddMessageListener(IMessageListener listener)
        {
            _messageHandler.AddListener(listener);
        }

        public void RemoveMessageListener(IMessageListener listener)
        {
            _messageHandler.RemoveListener(listener);
        }

        public void SendMessage(Message.Message message)
        {
            _messageHandler.SendMessage(message);
        }

        public abstract void Parse();
    }
}