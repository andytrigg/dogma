using dogma.Frontend;
using dogma.Intermediate;
using dogma.Message;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using tests.Frontend;

namespace tests.Backend
{
    internal class TestableBackend : dogma.Backend.Backend
    {
        public TestableBackend(IMessageHandler messageHandler) : base(messageHandler)
        {
        }

        public override void Process(IntermediateCode iCode, SymbolTable symTab)
        {
        }
    }
    
    [TestClass]
    public class BackendTest
    {
        [TestMethod]
        public void Backend_ShouldBeMessageProducer()
        {
            typeof(dogma.Backend.Backend).Should().Implement(typeof(IMessageProducer));
        }

        [TestMethod]
        public void AddMessageListener_ShouldRegisterAMessageListener()
        {
            var mockMessageHandler = new Mock<IMessageHandler>();
            var backend = new TestableBackend(mockMessageHandler.Object);

            var messageListener = Mock.Of<IMessageListener>();
            backend.AddMessageListener(messageListener);

            mockMessageHandler.Verify(handler => handler.AddListener(messageListener));
        }

        [TestMethod]
        public void RemoveMessageListener_ShouldRemoveAMessageListener()
        {
            var mockMessageHandler = new Mock<IMessageHandler>();
            var backend = new TestableBackend(mockMessageHandler.Object);

            var messageListener = Mock.Of<IMessageListener>();
            backend.RemoveMessageListener(messageListener);

            mockMessageHandler.Verify(handler => handler.RemoveListener(messageListener));
        }

        [TestMethod]
        public void SendMessage_ShouldSendMessage()
        {
            var mockMessageHandler = new Mock<IMessageHandler>();
            var backend = new TestableBackend(mockMessageHandler.Object);

            var message = Mock.Of<IMessage>();
            backend.SendMessage(message);

            mockMessageHandler.Verify(handler => handler.SendMessage(message));
        }
    }
}