using System;
namespace dogma.Frontend
{
    public abstract class Scanner
    {
        private Token currentToken;
        private Source source;

        public Scanner(Source source)
        {
            this.source = source;
        }
    }
}
