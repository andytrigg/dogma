using dogma.Intermediate;
using dogma.Message;
using dogma.Timing;

namespace dogma.Backend.Compiler
{
    public class CodeGenerator:Backend
    {
        private readonly ITimeProvider _timeProvider;

        public CodeGenerator(IMessageHandler messageHandler, ITimeProvider timeProvider) : base(messageHandler)
        {
            _timeProvider = timeProvider;
        }

        public override void Process(IntermediateCode iCode, SymbolTable symTab)
        {
            var startTime = _timeProvider.NowAsUnixTimeMilliseconds();

            int instructionCount = 0;
            // Send the parser summary message. 
            SendMessage(new Message.Message(MessageType.COMPILER_SUMMARY,
                new object[] {instructionCount, _timeProvider.ElapsedTimeSinceInSeconds(startTime)}));
        }
    }
}