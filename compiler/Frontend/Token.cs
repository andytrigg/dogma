﻿namespace dogma.Frontend
{
    public abstract class Token
    {
        public Token(ISource source)
        {
            Source = source;
            LineNumber = source.LineNumber;
        }

        protected ISource Source { get; }
        public int LineNumber { get; }

        public abstract void Extract();
    }

    public class EofToken : Token
    {
        public EofToken(ISource source) : base(source)
        {
        }

        public override void Extract()
        {
        }
    }

    public class CharToken : Token
    {
        public CharToken(ISource source) : base(source)
        {
        }

        public string Text { get; private set; }

        public override void Extract()
        {
            Text = char.ToString(Source.NextChar());
        }
    }
}