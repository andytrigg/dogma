using System;
using dogma.Frontend;
using dogma.Frontend.Pascal;
using dogma.Message;
using dogma.Timing;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace tests.Frontend
{
    [TestClass]
    public class FrontendFactoryTest
    {
        [TestMethod]
        public void CreateParser_PascalForTopDownParser()
        {
            var frontendFactory = new FrontendFactory(Mock.Of<IMessageHandler>(), Mock.Of<ITimeProvider>());
            var parser = frontendFactory.CreateParser("pascal", "top-down", Mock.Of<ISource>());

            parser.Should().BeOfType(typeof(PascalTopDownParser));
        }
        
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void CreateParser_ThrowsExceptionForUnknownLanguage()
        {
            var frontendFactory = new FrontendFactory(Mock.Of<IMessageHandler>(), Mock.Of<ITimeProvider>());
            frontendFactory.CreateParser("ruby", "top-down", Mock.Of<ISource>());
        }
        
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void CreateParser_ThrowsExceptionForUnknownType()
        {
            var frontendFactory = new FrontendFactory(Mock.Of<IMessageHandler>(), Mock.Of<ITimeProvider>());
            frontendFactory.CreateParser("pascal", "bottom-up", Mock.Of<ISource>());
        }
    }
}