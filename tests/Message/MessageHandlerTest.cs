using dogma.Message;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace tests.Message
{
    [TestClass]
    public class MessageHandlerTest
    {
        [TestMethod]
        public void SendMessage_WhenListenersProvided_NotifiesAllListeners()
        {
            var messageHandler = new MessageHandler();
            var mockListenerOne = new Mock<IMessageListener>();
            var mockListenerTwo = new Mock<IMessageListener>();
            messageHandler.AddListener(mockListenerOne.Object);
            messageHandler.AddListener(mockListenerTwo.Object);

            var fakeMessage = Mock.Of<IMessage>();
            messageHandler.SendMessage(fakeMessage);
            
            mockListenerOne.Verify(listener => listener.MessageReceived(fakeMessage), Times.Once);
            mockListenerTwo.Verify(listener => listener.MessageReceived(fakeMessage), Times.Once);
        }
        
        [TestMethod]
        public void SendMessage_WhenListenersRemoved_ShouldNotNotifyListners()
        {
            var messageHandler = new MessageHandler();
            var mockListenerOne = new Mock<IMessageListener>();
            messageHandler.AddListener(mockListenerOne.Object);
            messageHandler.RemoveListener(mockListenerOne.Object);

            var fakeMessage = Mock.Of<IMessage>();
            messageHandler.SendMessage(fakeMessage);
            
            mockListenerOne.Verify(listener => listener.MessageReceived(It.IsAny<IMessage>()), Times.Never);
        }
        
    }
}