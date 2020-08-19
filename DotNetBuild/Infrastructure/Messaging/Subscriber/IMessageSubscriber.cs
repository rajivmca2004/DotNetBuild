using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBuild.Infrastructure.Messaging.Subscriber
{
    public interface IMessageSubscriber
{
        string Subscribe();
    }
}
