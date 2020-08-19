using System.Collections.Generic;

namespace DotNetBuild.Services.Interfaces
{
    public interface IMessagePublisherService
{
    
        void Publish(string publishMessage);

    }
}
