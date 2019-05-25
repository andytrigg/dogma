using System.Linq;
using dogma.Frontend;
using dogma.Frontend.Pascal;
using dogma.Message;
using dogma.Timing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace tests.Frontend.Pascal
{
    [TestClass]
    public class PascalTopDownParserTest
    {
        [TestMethod]
        public void Parse_ShouldSendParserSummaryMessageWhenFinished()
        {
            const MessageType expectedMessageType = MessageType.PARSER_SUMMARY;
            var mockMessageHandler = new Mock<IMessageHandler>();
            var parser = new PascalTopDownParser(Mock.Of<IScanner>(scanner => scanner.NextToken() == new EofToken(Mock.Of<ISource>())), mockMessageHandler.Object, Mock.Of<ITimeProvider>());
            
            parser.Parse();
            
            mockMessageHandler.Verify(handler => handler.SendMessage(It.Is<dogma.Message.Message>(m => m.Type == expectedMessageType)));
        } 
        
        [TestMethod]
        public void Parse_ShouldSendParserSummaryMessageWhenFinishedWithCorrectLineNUmber()
        {
            const int expectedLineNumber = 973;
            var mockMessageHandler = new Mock<IMessageHandler>();
            var parser = new PascalTopDownParser(Mock.Of<IScanner>(scanner => scanner.NextToken() == new EofToken(Mock.Of<ISource>(s => s.LineNumber == expectedLineNumber))), mockMessageHandler.Object, Mock.Of<ITimeProvider>());
            
            parser.Parse();
            
            mockMessageHandler.Verify(handler => handler.SendMessage(It.Is<dogma.Message.Message>(m => ((object[])m.Body).SequenceEqual(new object[] {expectedLineNumber, 0L, 0f}))));
        } 
        
        [TestMethod]
        public void Parse_ShouldSendParserSummaryMessageWhenFinishedWithCorrectErrorCount()
        {
            const long expectedErrorCount = 0L;
            var mockMessageHandler = new Mock<IMessageHandler>();
            var parser = new PascalTopDownParser(Mock.Of<IScanner>(scanner => scanner.NextToken() == new EofToken(Mock.Of<ISource>())), mockMessageHandler.Object, Mock.Of<ITimeProvider>());

            parser.Parse();

            mockMessageHandler.Verify(handler => handler.SendMessage(It.Is<dogma.Message.Message>(m => ((object[])m.Body).SequenceEqual(new object[] {0, expectedErrorCount, 0f}))));
        }        
        
        [TestMethod]
        public void Parse_ShouldSendParserSummaryMessageWhenFinishedWithCorrectElapsedTime()
        {
            const float expectedElapsedTime = 10f;
            var mockMessageHandler = new Mock<IMessageHandler>();
            var mockTimeProvider = new Mock<ITimeProvider>();
            mockTimeProvider.Setup(tp => tp.NowAsUnixTimeMilliseconds()).Returns(1000L);
            mockTimeProvider.Setup(tp => tp.ElapsedTimeSinceInSeconds(1000L)).Returns(expectedElapsedTime);


            var parser = new PascalTopDownParser(Mock.Of<IScanner>(scanner => scanner.NextToken() == new EofToken(Mock.Of<ISource>())), mockMessageHandler.Object, mockTimeProvider.Object);
            
            parser.Parse();
            
            mockMessageHandler.Verify(handler => handler.SendMessage(It.Is<dogma.Message.Message>(m => ((object[])m.Body).SequenceEqual(new object[] {0, 0L, expectedElapsedTime}))));
        }         
//            
//        public override void Parse()
//        {
//            Token token;
//
//            var startTime = _timeProvider.NowAsUnixTimeMilliseconds();
//            while (!((token = Scanner.NextToken()) is EofToken))
//            {
//            }
//
//            // Send the parser summary message. 
//            SendMessage(new Message.Message(MessageType.PARSER_SUMMARY,
//                );
//        }
        
    }
}