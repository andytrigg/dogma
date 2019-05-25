using System.IO;
using dogma.Message;

namespace dogma.Frontend
{
    public static class Constants
    {
        public const char EOL = '\n';
        public const char EOF = (char) 0;
    }

    public interface ISource
    {
        int LineNumber { get; }
        char NextChar();
        char CurrentChar();
    }

    public class Source : ISource, IMessageProducer
    {
        public const int START_OF_LINE = -1;
        public const int START_OF_SOURCE = -2;
        private readonly IMessageHandler _messageHandler;
        private readonly TextReader reader;

        public Source(TextReader reader, IMessageHandler messageHandler)
        {
            LineNumber = 0;
            LinePosition = START_OF_SOURCE;
            this.reader = reader;
            _messageHandler = messageHandler;
        }

        private string Line { get; set; }
        public int LinePosition { get; private set; }

        public int LineNumber { get; private set; }

        public char CurrentChar()
        {
            if (Line == null)
                return Constants.EOF;
            if (LinePosition == START_OF_LINE || LinePosition == Line.Length) return Constants.EOL;
            return Line[LinePosition];
        }

        public char NextChar()
        {
            if (LinePosition == START_OF_SOURCE || LinePosition > Line.Length - 1)
                ReadLine();

            ++LinePosition;

            return CurrentChar();
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

        private void ReadLine()
        {
            Line = reader.ReadLine();
            LinePosition = START_OF_LINE;
            if (Line != null) ++LineNumber;


            if (Line != null)
                SendMessage(new Message.Message(MessageType.SOURCE_LINE, new object[] {LineNumber, Line}));
        }
    }
}