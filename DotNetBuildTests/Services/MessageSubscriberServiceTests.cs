using Xunit;
using Moq;
using DotNetBuild.Infrastructure.Messaging.Subscriber;
using DotNetBuild.Services;

namespace DotNetBuildTests.Services
{
    public class MessageSubscriberServiceTests
    {
        Mock<IMessageSubscriber> MockMessageSubscriber = null;
        [Fact]
        public void Setup_Positive()
        {
            // Assign
            string ExpectedResult = "Sample Message";
            MockMessageSubscriber = new Mock<IMessageSubscriber>();
            MockMessageSubscriber.Setup(x => x.Subscribe()).Returns(ExpectedResult);

            // Act
            MessageSubscriberService messageSubscriberService = new MessageSubscriberService(MockMessageSubscriber.Object);
            string ActualResult = messageSubscriberService.Subscribe();

            // Assert
            Assert.Equal(ExpectedResult, ActualResult);
        }
    }
}
