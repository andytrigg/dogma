using System;
using dogma.Frontend;
using dogma.Message;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace tests.Frontend
{
    internal class TestableParser : Parser
    {
        public TestableParser(IScanner scanner, IMessageHandler messageHandler) : base(scanner, messageHandler)
        {
        }

        public override void Parse()
        {
        }
    }

    [TestClass]
    public class ParserTest
    {
        [TestMethod]
        public void Parser_ShouldBeMessageProducer()
        {
            typeof(Parser).Should().Implement(typeof(IMessageProducer));
        }

        [TestMethod]
        public void AddMessageListener_ShouldRegisterAMessageListener()
        {
            var mockMessageHandler = new Mock<IMessageHandler>();
            var parser = new TestableParser(Mock.Of<IScanner>(), mockMessageHandler.Object);

            var messageListener = Mock.Of<IMessageListener>();
            parser.AddMessageListener(messageListener);

            mockMessageHandler.Verify(handler => handler.AddListener(messageListener));
        }

        [TestMethod]
        public void RemoveMessageListener_ShouldRemoveAMessageListener()
        {
            var mockMessageHandler = new Mock<IMessageHandler>();
            var parser = new TestableParser(Mock.Of<IScanner>(), mockMessageHandler.Object);

            var messageListener = Mock.Of<IMessageListener>();
            parser.RemoveMessageListener(messageListener);

            mockMessageHandler.Verify(handler => handler.RemoveListener(messageListener));
        }

        [TestMethod]
        public void SendMessage_ShouldSendMessage()
        {
            var mockMessageHandler = new Mock<IMessageHandler>();
            var parser = new TestableParser(Mock.Of<IScanner>(), mockMessageHandler.Object);

            var message = Mock.Of<IMessage>();
            parser.SendMessage(message);

            mockMessageHandler.Verify(handler => handler.SendMessage(message));
        }
    }
}