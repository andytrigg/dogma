using System;
using System.Collections.Generic;
using System.Text;

namespace dogma.Frontend.Pascal
{
    class PascalScanner : Scanner
    {
        public PascalScanner(ISource source) : base(source)
        {
        }

        protected override Token ExtractToken()
        {
            Token token;
            char currentChar = this.CurrentChar();

            // Construct the next token.  The current character determines the
            // token type.
            if (currentChar == Constants.EOF)
            {
                token = new EofToken(this.Source);
            }
            else
            {
                token = new CharToken(Source);
            }

            return token;
        }
    }
}
