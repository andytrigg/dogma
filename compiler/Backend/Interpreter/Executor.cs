using dogma.Intermediate;
using dogma.Message;
using dogma.Timing;

namespace dogma.Backend.Interpreter
{
    public class Executor:Backend
    {
        private readonly ITimeProvider _timeProvider;
        
        public Executor(IMessageHandler messageHandler, ITimeProvider timeProvider) : base(messageHandler)
        {
            _timeProvider = timeProvider;
        }

        public override void Process(IntermediateCode iCode, SymbolTable symTab)
        {
            var startTime = _timeProvider.NowAsUnixTimeMilliseconds();

            var executionCount = 0;
            var runtimeErrors = 0;
            
            SendMessage(new Message.Message(MessageType.INTERPRETER_SUMMARY,
                new object[] {executionCount, runtimeErrors, _timeProvider.ElapsedTimeSinceInSeconds(startTime)}));

        }
    }
}