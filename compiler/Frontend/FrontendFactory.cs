using System;
using dogma.Frontend.Pascal;
using dogma.Message;
using dogma.Timing;

namespace dogma.Frontend
{
    public class FrontendFactory
    {
        private readonly IMessageHandler _messageHandler;
        private readonly ITimeProvider _timeProvider;

        public FrontendFactory(IMessageHandler messageHandler, ITimeProvider timeProvider)
        {
            _messageHandler = messageHandler;
            _timeProvider = timeProvider;
        }

        public Parser CreateParser(string language, string type, ISource source)
        {
            if ("pascal".Equals(language.ToLower()) && "top-down".Equals(type.ToLower()))
            {
                return new PascalTopDownParser(new PascalScanner(source), _messageHandler, _timeProvider);
            }
            throw new NotSupportedException("Specified language and type combination is not supported");
        }
    }
}