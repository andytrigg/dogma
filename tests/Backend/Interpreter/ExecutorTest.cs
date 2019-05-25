using System.Linq;
using dogma.Backend.Compiler;
using dogma.Backend.Interpreter;
using dogma.Intermediate;
using dogma.Message;
using dogma.Timing;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace tests.Backend.Interpreter
{
    [TestClass]
    public class ExecutorTest
    {
        [TestMethod]
        public void Executor_ShouldBeMessageProducer()
        {
            typeof(Executor).Should().Implement(typeof(IMessageProducer));
        }

        [TestMethod]
        public void AddMessageListener_ShouldRegisterAMessageListener()
        {
            var mockMessageHandler = new Mock<IMessageHandler>();
            var executor = new Executor(mockMessageHandler.Object, Mock.Of<ITimeProvider>());

            var messageListener = Mock.Of<IMessageListener>();
            executor.AddMessageListener(messageListener);

            mockMessageHandler.Verify(handler => handler.AddListener(messageListener));
        }

        [TestMethod]
        public void RemoveMessageListener_ShouldRemoveAMessageListener()
        {
            var mockMessageHandler = new Mock<IMessageHandler>();
            var executor = new Executor(mockMessageHandler.Object, Mock.Of<ITimeProvider>());

            var messageListener = Mock.Of<IMessageListener>();
            executor.RemoveMessageListener(messageListener);

            mockMessageHandler.Verify(handler => handler.RemoveListener(messageListener));
        }

        [TestMethod]
        public void SendMessage_ShouldSendMessage()
        {
            var mockMessageHandler = new Mock<IMessageHandler>();
            var executor = new Executor(mockMessageHandler.Object, Mock.Of<ITimeProvider>());

            var message = new dogma.Message.Message(MessageType.SYNTAX_ERROR, null);
            executor.SendMessage(message);

            mockMessageHandler.Verify(handler => handler.SendMessage(message));
        }

        [TestMethod]
        public void Process_ShouldSendInterpreterSummaryMessageWhenFinished()
        {
            const MessageType expectedMessageType = MessageType.INTERPRETER_SUMMARY;
            var mockMessageHandler = new Mock<IMessageHandler>();
            var executor = new Executor(mockMessageHandler.Object, Mock.Of<ITimeProvider>());
            
            executor.Process(Mock.Of<IntermediateCode>(), Mock.Of<SymbolTable>());
            
            mockMessageHandler.Verify(handler => handler.SendMessage(It.Is<dogma.Message.Message>(m => m.Type == expectedMessageType)));
        } 
        
        [TestMethod]
        public void Process_ShouldSendInterpreterSummaryMessageWhenFinishedWithCorrectExecutionCount()
        {
            const int expectedExecutionCount = 0;
            var mockMessageHandler = new Mock<IMessageHandler>();
            var executor = new Executor(mockMessageHandler.Object, Mock.Of<ITimeProvider>());
            
            executor.Process(Mock.Of<IntermediateCode>(), Mock.Of<SymbolTable>());
            
            mockMessageHandler.Verify(handler => handler.SendMessage(It.Is<dogma.Message.Message>(m => ((object[])m.Body).SequenceEqual(new object[] {expectedExecutionCount, 0, 0f}))));
        }        
        
        [TestMethod]
        public void Process_ShouldSendInterpreterSummaryMessageWhenFinishedWithCorrectRuntimeErrors()
        {
            const int expectedRuntimeErrors = 0;
            var mockMessageHandler = new Mock<IMessageHandler>();
            var executor = new Executor(mockMessageHandler.Object, Mock.Of<ITimeProvider>());
            
            executor.Process(Mock.Of<IntermediateCode>(), Mock.Of<SymbolTable>());
            
            mockMessageHandler.Verify(handler => handler.SendMessage(It.Is<dogma.Message.Message>(m => ((object[])m.Body).SequenceEqual(new object[] {0, expectedRuntimeErrors, 0f}))));
        }

        [TestMethod]
        public void Parse_ShouldSendInterpreterSummaryMessageWhenFinishedWithCorrectElapsedTime()
        {
            const float expectedElapsedTime = 10f;
            var mockMessageHandler = new Mock<IMessageHandler>();
            var mockTimeProvider = new Mock<ITimeProvider>();
            mockTimeProvider.Setup(tp => tp.NowAsUnixTimeMilliseconds()).Returns(1000L);
            mockTimeProvider.Setup(tp => tp.ElapsedTimeSinceInSeconds(1000L)).Returns(expectedElapsedTime);
            
            var executor = new Executor(mockMessageHandler.Object, mockTimeProvider.Object);

            executor.Process(Mock.Of<IntermediateCode>(), Mock.Of<SymbolTable>());
            
            mockMessageHandler.Verify(handler => handler.SendMessage(It.Is<dogma.Message.Message>(m => ((object[])m.Body).SequenceEqual(new object[] {0, 0, expectedElapsedTime}))));
        }     
    }
}