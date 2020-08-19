using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using DotNetBuild.Domain.Models;

namespace DotNetBuild.Infrastructure.Messaging.Subscriber
{
    public class RabbitMQSubscriber : IMessageSubscriber
    {
        private IOptions<MQConnectionData> app = null;
        private readonly string queueName = string.Empty;
        private readonly IConnectionFactory _factory = null;
        public RabbitMQSubscriber(IConnectionFactory factory, IOptions<MQConnectionData> _app)
        {
            _factory = factory;
            app = _app;
            queueName = app.Value.QueueName;
        }

        public string Subscribe()
        {
            string message = string.Empty;

            // CloudAMQP URL in format amqp://user:pass@hostName:port/vhost
            string url = $"amqps://{app.Value.UserName}:{app.Value.Password}@{app.Value.HostName}/{app.Value.vHost}";
            _factory.Uri = new Uri(url);
            using (IConnection connection = _factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, true, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    BasicGetResult result = channel.BasicGet(queueName, true);
                    if (result != null)
                    {
                        message =
                        Encoding.UTF8.GetString(result.Body);
                    }
                }
            }
            return message;
        }
    }
}
