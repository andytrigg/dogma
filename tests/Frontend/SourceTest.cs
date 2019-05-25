using System.IO;
using dogma.Frontend;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tests.Frontend
{
    [TestClass]
    public class SourceTest
    {
        [TestMethod]
        public void WhenInitialised_LinePosition_ShouldBeStartOfSource()
        {
            new Source(new StringReader("")).LinePosition.Should().Be(Source.START_OF_SOURCE);
        }

        [TestMethod]
        public void WhenInitialised_LineNUmber_ShouldBeZero()
        {
            new Source(new StringReader("")).LineNumber.Should().Be(0);
        }

        [TestMethod]
        public void CurrentChar_WhenAtEndOfSource_ReturnsEof()
        {
            new Source(new StringReader("")).CurrentChar().Should().Be(Constants.EOF);
        }

        [TestMethod]
        public void CurrentChar_WhenAtEndOfLine_ReturnsEol()
        {
            ISource source = new Source(new StringReader("A\nB"));
            source.NextChar();
            source.NextChar();
            source.CurrentChar().Should().Be(Constants.EOL);
        }

        [TestMethod]
        public void CurrentChar_WhenMidLine_ReturnsExpectedChar()
        {
            ISource source = new Source(new StringReader("A"));
            source.NextChar();
            source.CurrentChar().Should().Be('A');
        }

        [TestMethod]
        public void NextChar_WhenAtStartOfSource_ReturnsFirstCharacter()
        {
            ISource source = new Source(new StringReader("A\nB"));
            source.NextChar().Should().Be('A');
        }

        [TestMethod]
        public void NextChar_WhenAtEndOfLine_ReturnsEol()
        {
            ISource source = new Source(new StringReader("A\nB"));
            source.NextChar();
            source.NextChar().Should().Be(Constants.EOL);
        }

        [TestMethod]
        public void NextChar_WhenAtEndOfLine_SubsequentNextCharReturnsFirstCharOfNextLine()
        {
            ISource source = new Source(new StringReader("A\nB"));
            source.NextChar();
            source.NextChar();
            source.NextChar().Should().Be('B');
        }

        [TestMethod]
        public void NextChar_WhenAtEndOfSource_ReturnsEof()
        {
            var source = new Source(new StringReader("A"));
            source.NextChar();
            source.NextChar();
            source.NextChar().Should().Be(Constants.EOF);
        }
    }
}