using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;
using DotNetBuild.Domain.Models;

namespace DotNetBuild.Infrastructure.Messaging.Publisher
{
    public class RabbitMQPublisher : IMessagePublisher
{
    private IConnectionFactory _factory;
    private IOptions<MQConnectionData> app = null;
    private readonly string queueName = string.Empty;

    public RabbitMQPublisher(IConnectionFactory factory, IOptions<MQConnectionData> _app)
    {
        _factory = factory;
        app = _app;
        queueName = app.Value.QueueName;
    }

    public void Publish(string message)
    {
        // CloudAMQP URL in format amqp://user:pass@hostName:port/vhost
        string url = $"amqps://{app.Value.UserName}:{app.Value.Password}@{app.Value.HostName}/{app.Value.vHost}";
        _factory.Uri = new Uri(url);

        using (var connection = _factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            //the queue
            channel.QueueDeclare(queue: queueName,
                         durable: true,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

            // publisher
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
}
