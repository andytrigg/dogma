using System;
using System.IO;
using dogma.Message;

namespace dogma.Frontend
{
    public interface ISource
    {
        char NextChar();
        char CurrentChar();
    }

    public class Source : ISource, MessageProducer
    {
        public const char EOL = '\n';
        public const char EOF = (char)0;
        public const int START_OF_LINE = -1;
        public const int START_OF_SOURCE = -2;
        private TextReader reader;

        public int LineNumber { get; private set; }
        private string Line { get; set; }
        public int LinePosition { get; private set; }

        public Source(TextReader reader)
        {
            this.LineNumber = 0;
            this.LinePosition = START_OF_SOURCE;
            this.reader = reader;
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
                SendMessage(new Message(MessageType.SOURCE_LINE, new object[] { LineNumber, Line })); 
            }
        }

        public char CurrentChar()
        {
            if (Line == null)
            {
                return EOF;
            }
            else if (LinePosition == START_OF_LINE || LinePosition == Line.Length)
            {
                return EOL;
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
            throw new NotImplementedException();
        }

        public void RemoveMessageListener(MessageListener listener)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(Message.Message message)
        {
            throw new NotImplementedException();
        }
    }
}
