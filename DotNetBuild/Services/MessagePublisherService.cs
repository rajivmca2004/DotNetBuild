using DotNetBuild.Infrastructure.Messaging.Publisher;
using DotNetBuild.Services.Interfaces;

namespace DotNetBuild.Services
{
   public class MessagePublisherService : IMessagePublisherService
{
        protected readonly IMessagePublisher _messagePublisher;
        public MessagePublisherService(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }
    
        public void Publish(string message)
        {
             _messagePublisher.Publish(message);
        }
    }
}
