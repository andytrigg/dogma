using System;
using dogma.Backend.Compiler;
using dogma.Backend.Interpreter;
using dogma.Message;
using dogma.Timing;

namespace dogma.Backend
{
    public class BackendFactory
    {
        private readonly IMessageHandler _messageHandler;
        private readonly ITimeProvider _timeProvider;

        public BackendFactory(IMessageHandler messageHandler, ITimeProvider timeProvider)
        {
            _messageHandler = messageHandler;
            _timeProvider = timeProvider;
        }

        public Backend CreateBackend(string operation)
        {
            switch (operation.ToLower())
            {
                case "compile":
                    return new CodeGenerator(_messageHandler, _timeProvider);
                case "execute":
                    return new Executor(_messageHandler, _timeProvider);
                default:
                    throw new NotSupportedException("Specified operation is not supported");
            }
        }
    }
}