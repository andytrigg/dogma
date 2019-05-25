using System.Linq;
using dogma.Backend.Compiler;
using dogma.Intermediate;
using dogma.Message;
using dogma.Timing;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace tests.Backend.Compiler
{
    [TestClass]
    public class CodeGeneratorTest
    {
        [TestMethod]
        public void CodeGenerator_ShouldBeMessageProducer()
        {
            typeof(CodeGenerator).Should().Implement(typeof(IMessageProducer));
        }

        [TestMethod]
        public void AddMessageListener_ShouldRegisterAMessageListener()
        {
            var mockMessageHandler = new Mock<IMessageHandler>();
            var codeGenerator = new CodeGenerator(mockMessageHandler.Object, Mock.Of<ITimeProvider>());

            var messageListener = Mock.Of<IMessageListener>();
            codeGenerator.AddMessageListener(messageListener);

            mockMessageHandler.Verify(handler => handler.AddListener(messageListener));
        }

        [TestMethod]
        public void RemoveMessageListener_ShouldRemoveAMessageListener()
        {
            var mockMessageHandler = new Mock<IMessageHandler>();
            var codeGenerator = new CodeGenerator(mockMessageHandler.Object, Mock.Of<ITimeProvider>());

            var messageListener = Mock.Of<IMessageListener>();
            codeGenerator.RemoveMessageListener(messageListener);

            mockMessageHandler.Verify(handler => handler.RemoveListener(messageListener));
        }

        [TestMethod]
        public void SendMessage_ShouldSendMessage()
        {
            var mockMessageHandler = new Mock<IMessageHandler>();
            var codeGenerator = new CodeGenerator(mockMessageHandler.Object, Mock.Of<ITimeProvider>());

            var message = new dogma.Message.Message(MessageType.SYNTAX_ERROR, null);
            codeGenerator.SendMessage(message);

            mockMessageHandler.Verify(handler => handler.SendMessage(message));
        }

        [TestMethod]
        public void Process_ShouldSendCompilerSummaryMessageWhenFinished()
        {
            const MessageType expectedMessageType = MessageType.COMPILER_SUMMARY;
            var mockMessageHandler = new Mock<IMessageHandler>();
            var codeGenerator = new CodeGenerator(mockMessageHandler.Object, Mock.Of<ITimeProvider>());
            
            codeGenerator.Process(Mock.Of<IntermediateCode>(), Mock.Of<SymbolTable>());
            
            mockMessageHandler.Verify(handler => handler.SendMessage(It.Is<dogma.Message.Message>(m => m.Type == expectedMessageType)));
        } 
        
        [TestMethod]
        public void Process_ShouldSendCompilerSummaryMessageWhenFinishedWithCorrectInstructionCount()
        {
            const int expectedInstructionCount = 0;
            var mockMessageHandler = new Mock<IMessageHandler>();
            var codeGenerator = new CodeGenerator(mockMessageHandler.Object, Mock.Of<ITimeProvider>());
            
            codeGenerator.Process(Mock.Of<IntermediateCode>(), Mock.Of<SymbolTable>());
            
            mockMessageHandler.Verify(handler => handler.SendMessage(It.Is<dogma.Message.Message>(m => ((object[])m.Body).SequenceEqual(new object[] {expectedInstructionCount, 0f}))));
        }

        [TestMethod]
        public void Parse_ShouldSendCompilerSummaryMessageWhenFinishedWithCorrectElapsedTime()
        {
            const float expectedElapsedTime = 10f;
            var mockMessageHandler = new Mock<IMessageHandler>();
            var mockTimeProvider = new Mock<ITimeProvider>();
            mockTimeProvider.Setup(tp => tp.NowAsUnixTimeMilliseconds()).Returns(1000L);
            mockTimeProvider.Setup(tp => tp.ElapsedTimeSinceInSeconds(1000L)).Returns(expectedElapsedTime);
            
            var codeGenerator = new CodeGenerator(mockMessageHandler.Object, mockTimeProvider.Object);

            codeGenerator.Process(Mock.Of<IntermediateCode>(), Mock.Of<SymbolTable>());
            
            mockMessageHandler.Verify(handler => handler.SendMessage(It.Is<dogma.Message.Message>(m => ((object[])m.Body).SequenceEqual(new object[] {0, expectedElapsedTime}))));
        }     
    }
}