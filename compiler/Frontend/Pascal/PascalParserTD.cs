using dogma.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace dogma.Frontend.Pascal
{
    class PascalParserTD : Parser
    {
        public PascalParserTD(Scanner scanner) : base(scanner)
        {
        }

        public override void Parse()
        {
            Token token;

            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            while (!((token = this.Scanner.NextToken()) is EofToken)) {
            }
            // Send the parser summary message. 
            float elapsedTime = (DateTimeOffset.Now.ToUnixTimeMilliseconds() - startTime)/1000f;
            SendMessage(new Message.Message(MessageType.PARSER_SUMMARY, new object[] {token.LineNumber, GetErrorCount(), elapsedTime}));

        }

        private long GetErrorCount()                 
        {
            return 0;
        }
    }  
}
