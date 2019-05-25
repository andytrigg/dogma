using dogma.Message;
using dogma.Timing;

namespace dogma.Frontend.Pascal
{
    public class PascalTopDownParser : Parser
    {
        private readonly ITimeProvider _timeProvider;

        public PascalTopDownParser(IScanner scanner, IMessageHandler messageHandler, ITimeProvider timeProvider) : base(scanner, messageHandler)
        {
            _timeProvider = timeProvider;
        }

        public override void Parse()
        {
            Token token;

            var startTime = _timeProvider.NowAsUnixTimeMilliseconds();
            while (!((token = Scanner.NextToken()) is EofToken))
            {
            }

            // Send the parser summary message. 
            SendMessage(new Message.Message(MessageType.PARSER_SUMMARY,
                new object[] {token.LineNumber, GetErrorCount(), _timeProvider.ElapsedTimeSinceInSeconds(startTime)}));
        }


        private long GetErrorCount()
        {
            return 0;
        }
    }
}