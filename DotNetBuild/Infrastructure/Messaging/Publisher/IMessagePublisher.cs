
namespace DotNetBuild.Infrastructure.Messaging.Publisher
{
    public interface IMessagePublisher
    {
        void Publish(string message);
    }
}
