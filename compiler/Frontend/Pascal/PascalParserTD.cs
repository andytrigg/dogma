using System;
using dogma.Message;

namespace dogma.Frontend.Pascal
{
    internal class PascalParserTD : Parser
    {
        public PascalParserTD(IScanner scanner, IMessageHandler messageHandler) : base(scanner, messageHandler)
        {
        }

        public override void Parse()
        {
            Token token;

            var startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            while (!((token = Scanner.NextToken()) is EofToken))
            {
            }

            // Send the parser summary message. 
            var elapsedTime = (DateTimeOffset.Now.ToUnixTimeMilliseconds() - startTime) / 1000f;
            SendMessage(new Message.Message(MessageType.PARSER_SUMMARY,
                new object[] {token.LineNumber, GetErrorCount(), elapsedTime}));
        }

        private long GetErrorCount()
        {
            return 0;
        }
    }
}