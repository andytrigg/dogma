using dogma.Frontend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace tests.Frontend
{
    [TestClass]
    public class EofTokenTest
    {
        [TestMethod]
        public void Extract_DoesNothing()
        {
            var mockSource = new Mock<ISource>(MockBehavior.Strict);
            mockSource.Setup(source => source.LineNumber).Returns(0);
            
            var charToken = new EofToken(mockSource.Object);

            charToken.Extract();
        }
    }
}