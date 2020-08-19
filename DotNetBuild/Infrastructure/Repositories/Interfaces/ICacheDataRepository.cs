using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetBuild.Infrastructure.Repositories.Interfaces
{
    public interface ICacheDataRepository
    {
         Task<string> Get(string key);

         Task<string> Post(Dictionary<string, string> data);

         Task<string> Put(Dictionary<string, string> data);

         Task<string> Delete(string key);
    }
}
