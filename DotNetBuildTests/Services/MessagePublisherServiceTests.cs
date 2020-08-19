using Xunit;
using Moq;
using DotNetBuild.Infrastructure.Messaging.Publisher;
using DotNetBuild.Services;
using System;

namespace DotNetBuildTests.Services
{
    public class MessagePublisherServiceTests
    {
        Mock<IMessagePublisher> MockMessagePublisher = null;
    public MessagePublisherServiceTests()
        {
            MockMessagePublisher = new Mock<IMessagePublisher>();
        }

         [Fact]
        public void Publish_Positive()
        {
            // Assign
            MockMessagePublisher.Setup(x => x.Publish(It.IsAny<string>()));

            // Act
            MessagePublisherService messagePublisherService = new MessagePublisherService(MockMessagePublisher.Object);
            messagePublisherService.Publish("Sample Message");

            // Assert
            // No Retun object exists
        }
    }
}
