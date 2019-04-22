using System;
namespace dogma.Frontend
{
    public abstract class Token
    {
        public Token(ISource source)
        {
            this.Source = source;
        }

        protected ISource Source { get; private set; }

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
            Text = Char.ToString(this.Source.NextChar());
        }
    }
}
