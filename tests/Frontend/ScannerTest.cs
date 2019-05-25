using dogma.Frontend;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace tests.Frontend
{
    internal class TestableScanner : Scanner
    {
        public TestableScanner(ISource source) : base(source)
        {
        }

        protected override Token ExtractToken()
        {
            return null;
        }
    }
    
    [TestClass]
    public class ScannerTest
    {
        [TestMethod]
        public void CurrentChar_ShouldReturnCurrentChar()
        {
            const char expectedCurrentChar = 'a';

            var scanner = new TestableScanner(Mock.Of<ISource>(source => source.CurrentChar() == expectedCurrentChar));
            
            scanner.CurrentChar().Should().Be(expectedCurrentChar);
        }
        
        [TestMethod]
        public void NextChar_ShouldReturnNextChar()
        {
            const char expectedNextChar = 'a';

            var scanner = new TestableScanner(Mock.Of<ISource>(source => source.NextChar() == expectedNextChar));
            
            scanner.NextChar().Should().Be(expectedNextChar);
        }
    }
}