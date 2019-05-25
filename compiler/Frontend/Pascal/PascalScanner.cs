namespace dogma.Frontend.Pascal
{
    internal class PascalScanner : Scanner
    {
        public PascalScanner(ISource source) : base(source)
        {
        }

        protected override Token ExtractToken()
        {
            Token token;
            var currentChar = CurrentChar();

            // Construct the next token.  The current character determines the
            // token type.
            if (currentChar == Constants.EOF)
                token = new EofToken(Source);
            else
                token = new CharToken(Source);

            return token;
        }
    }
}