using DotNetBuild.Infrastructure.Messaging.Subscriber;
using DotNetBuild.Services.Interfaces;

namespace DotNetBuild.Services
{
   public class MessageSubscriberService : IMessageSubscriberService
{
        protected readonly IMessageSubscriber _messageSubscriber;
        public MessageSubscriberService(IMessageSubscriber messageSubscriber)
        {
            _messageSubscriber = messageSubscriber;
        }

        public string Subscribe()
        {
             return   _messageSubscriber.Subscribe();
        }

    }
}
