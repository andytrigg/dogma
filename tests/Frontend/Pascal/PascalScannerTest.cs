using dogma.Frontend;
using dogma.Frontend.Pascal;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace tests.Frontend.Pascal
{
    [TestClass]
    public class PascalScannerTest
    {
        [TestMethod]
        public void CurrentChar_ShouldReturnCurrentChar()
        {
            const char expectedCurrentChar = 'a';

            var scanner = new PascalScanner(Mock.Of<ISource>(source => source.CurrentChar() == expectedCurrentChar));
            
            scanner.CurrentChar().Should().Be(expectedCurrentChar);
        }
        
        [TestMethod]
        public void NextChar_ShouldReturnNextChar()
        {
            const char expectedNextChar = 'a';

            var scanner = new PascalScanner(Mock.Of<ISource>(source => source.NextChar() == expectedNextChar));
            
            scanner.NextChar().Should().Be(expectedNextChar);
        }
        
        [TestMethod]
        public void NextToken_ShouldReturnEndOfFileTokenIfCurrentCharIsEndOfFile()
        {
            var scanner = new PascalScanner(Mock.Of<ISource>(source => source.CurrentChar() == Constants.EOF));
            
            scanner.NextToken().Should().BeOfType(typeof(EofToken));
        }
        
        [TestMethod]
        public void NextToken_ShouldReturnCharTokenIfCurrentCharIsNotEndOfFile()
        {
            var scanner = new PascalScanner(Mock.Of<ISource>(source => source.CurrentChar() == 'a'));
            
            scanner.NextToken().Should().BeOfType(typeof(CharToken));
        }        
    }
}