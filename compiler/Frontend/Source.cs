using System;
using System.IO;
using dogma.Message;

namespace dogma.Frontend
{
    public static class Constants
    {
        public const char EOL = '\n';
        public const char EOF = (char)0;
    }

    public interface ISource
    {
        char NextChar();
        char CurrentChar();

        int LineNumber { get; }
    }

    public class Source : ISource, MessageProducer
    {
        public const int START_OF_LINE = -1;
        public const int START_OF_SOURCE = -2;
        private TextReader reader;
        private MessageHandler messageHandler;

        public int LineNumber { get; private set; }
        private string Line { get; set; }
        public int LinePosition { get; private set; }

        public Source(TextReader reader)
        {
            this.LineNumber = 0;
            this.LinePosition = START_OF_SOURCE;
            this.reader = reader;
            this.messageHandler = new MessageHandler();
        }

        private void ReadLine()
        {
            Line = reader.ReadLine();
            LinePosition = START_OF_LINE;
            if (Line != null)
            {
                ++LineNumber;
            }


            if (Line != null) 
            { 
                SendMessage(new Message.Message(MessageType.SOURCE_LINE, new object[] { LineNumber, Line })); 
            }
        }

        public char CurrentChar()
        {
            if (Line == null)
            {
                return Constants.EOF;
            }
            else if (LinePosition == START_OF_LINE || LinePosition == Line.Length)
            {
                return Constants.EOL;
            }
            return Line[LinePosition];
        }

        public char NextChar()
        {
            if (LinePosition == START_OF_SOURCE || LinePosition > (Line.Length - 1))
                ReadLine();

            ++LinePosition;

            return CurrentChar();
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
    }
}
