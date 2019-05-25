namespace dogma.Frontend
{
    public interface IScanner
    {
        Token NextToken();
    }

    public abstract class Scanner : IScanner
    {
        private Token currentToken;

        public Scanner(ISource source)
        {
            Source = source;
        }

        protected ISource Source { get; }

        public Token NextToken()
        {
            currentToken = ExtractToken();
            return currentToken;
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