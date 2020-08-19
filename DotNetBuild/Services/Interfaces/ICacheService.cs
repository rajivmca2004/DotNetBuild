using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetBuild.Services.Interfaces
{
    public interface ICacheService
    {
             Task<string> Get(string key);

             Task<string> Post(Dictionary<string, string> data);

             Task<string> Put(Dictionary<string, string> data);

             Task<string> Delete(string key);
    }
}
