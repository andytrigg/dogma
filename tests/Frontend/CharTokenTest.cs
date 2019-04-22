using System;
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
            Mock<ISource> expectedResult = new Mock<ISource>();
            expectedResult.Setup(x => x.NextChar()).Returns('A');

            CharToken charToken = new CharToken(expectedResult.Object);

            charToken.Extract();

            charToken.Text.Should().Be("A");
        }
    }
}
