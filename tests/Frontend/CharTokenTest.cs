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
            var expectedResult = Mock.Of<ISource>(x => x.NextChar() == 'A');

            var charToken = new CharToken(expectedResult);

            charToken.Extract();

            charToken.Text.Should().Be("A");
        }
    }
}