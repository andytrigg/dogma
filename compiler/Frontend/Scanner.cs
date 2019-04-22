namespace dogma.Frontend
{
    public abstract class Scanner
    {
        private Token currentToken;

        protected ISource Source { get; private set; }

        public Scanner(ISource source)
        {
            this.Source = source;
        }

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




    