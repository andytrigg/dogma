using System;
using dogma.Intermediate;
using dogma.Message;

namespace dogma.Frontend
{
    public abstract class Parser : MessageProducer
    {
        private Scanner scanner;
        private IntermediateCode intermediateCode;
        private MessageHandler messageHandler = new MessageHandler();

        public Parser(Scanner scanner)
        {
            this.scanner = scanner;
            this.intermediateCode = null;
        }

        public void AddMessageListener(MessageListener listener)
        {
            messageHandler.AddListener(listener);
        }

        public void RemoveMessageListener(MessageListener listener)
        {
            messageHandler.RemoveListener(listener);
        }

        public void SendMessage(Message.Message message)
        {
            messageHandler.SendMessage(message);
        }

        public abstract void Parse();
    }
}
