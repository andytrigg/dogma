using System.IO;
using dogma.Frontend;
using dogma.Message;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace tests.Frontend
{
    [TestClass]
    public class SourceTest
    {
        [TestMethod]
        public void WhenInitialised_LinePosition_ShouldBeStartOfSource()
        {
            new Source(new StringReader(""), Mock.Of<IMessageHandler>()).LinePosition.Should().Be(Source.START_OF_SOURCE);
        }

        [TestMethod]
        public void WhenInitialised_LineNUmber_ShouldBeZero()
        {
            new Source(new StringReader(""), Mock.Of<IMessageHandler>()).LineNumber.Should().Be(0);
        }

        [TestMethod]
        public void CurrentChar_WhenAtEndOfSource_ReturnsEof()
        {
            new Source(new StringReader(""), Mock.Of<IMessageHandler>()).CurrentChar().Should().Be(Constants.EOF);
        }

        [TestMethod]
        public void CurrentChar_WhenAtEndOfLine_ReturnsEol()
        {
            ISource source = new Source(new StringReader("A\nB"), Mock.Of<IMessageHandler>());
            source.NextChar();
            source.NextChar();
            source.CurrentChar().Should().Be(Constants.EOL);
        }

        [TestMethod]
        public void CurrentChar_WhenMidLine_ReturnsExpectedChar()
        {
            ISource source = new Source(new StringReader("A"), Mock.Of<IMessageHandler>());
            source.NextChar();
            source.CurrentChar().Should().Be('A');
        }

        [TestMethod]
        public void NextChar_WhenAtStartOfSource_ReturnsFirstCharacter()
        {
            ISource source = new Source(new StringReader("A\nB"), Mock.Of<IMessageHandler>());
            source.NextChar().Should().Be('A');
        }

        [TestMethod]
        public void NextChar_WhenAtEndOfLine_ReturnsEol()
        {
            ISource source = new Source(new StringReader("A\nB"), Mock.Of<IMessageHandler>());
            source.NextChar();
            source.NextChar().Should().Be(Constants.EOL);
        }

        [TestMethod]
        public void NextChar_WhenAtEndOfLine_SubsequentNextCharReturnsFirstCharOfNextLine()
        {
            ISource source = new Source(new StringReader("A\nB"), Mock.Of<IMessageHandler>());
            source.NextChar();
            source.NextChar();
            source.NextChar().Should().Be('B');
        }

        [TestMethod]
        public void NextChar_WhenAtEndOfSource_ReturnsEof()
        {
            var source = new Source(new StringReader("A"), Mock.Of<IMessageHandler>());
            source.NextChar();
            source.NextChar();
            source.NextChar().Should().Be(Constants.EOF);
        }
        
        [TestMethod]
        public void Source_ShouldBeMessageProducer()
        {
            typeof(Source).Should().Implement(typeof(IMessageProducer));
        }

        [TestMethod]
        public void AddMessageListener_ShouldRegisterAMessageListener()
        {
            var mockMessageHandler = new Mock<IMessageHandler>();
            var source = new Source(new StringReader("A"), mockMessageHandler.Object);

            var messageListener = Mock.Of<IMessageListener>();
            source.AddMessageListener(messageListener);

            mockMessageHandler.Verify(handler => handler.AddListener(messageListener));
        }

        [TestMethod]
        public void RemoveMessageListener_ShouldRemoveAMessageListener()
        {
            var mockMessageHandler = new Mock<IMessageHandler>();
            var source = new Source(new StringReader("A"), mockMessageHandler.Object);

            var messageListener = Mock.Of<IMessageListener>();
            source.RemoveMessageListener(messageListener);

            mockMessageHandler.Verify(handler => handler.RemoveListener(messageListener));
        }

        [TestMethod]
        public void SendMessage_ShouldSendMessage()
        {
            var mockMessageHandler = new Mock<IMessageHandler>();
            var source = new Source(new StringReader("A"), mockMessageHandler.Object);

            var message = Mock.Of<IMessage>();
            source.SendMessage(message);

            mockMessageHandler.Verify(handler => handler.SendMessage(message));
        }        
    }
}