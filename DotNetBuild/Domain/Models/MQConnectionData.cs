
namespace DotNetBuild.Domain.Models
{
    public class MQConnectionData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }

        public string vHost { get; set; }

        public string QueueName { get; set; }

    }
}
