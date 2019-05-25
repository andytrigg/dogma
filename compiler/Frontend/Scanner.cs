namespace dogma.Frontend
{
    public interface IScanner
    {
        Token NextToken();
    }

    public abstract class Scanner : IScanner
    {
        private Token _currentToken;

        protected Scanner(ISource source)
        {
            Source = source;
        }

        protected ISource Source { get; }

        public Token NextToken()
        {
            _currentToken = ExtractToken();
            return _currentToken;
        }

        protected abstract Token ExtractToken();

        public char CurrentChar()
        {
            return Source.CurrentChar();
        }

        public char NextChar()
        {
            return Source.NextChar();
        }
    }
}