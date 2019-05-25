using dogma.Frontend;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace tests.Frontend
{
    [TestClass]
    public class CharTokenTest
    {
        [TestMethod]
        public void Extract_ReturnsCharTokenFromSource()
        {
            var mockSource = Mock.Of<ISource>(x => x.NextChar() == 'A');
        
            var charToken = new CharToken(mockSource);

            charToken.Extract();

            charToken.Text.Should().Be("A");
        }
    }
}