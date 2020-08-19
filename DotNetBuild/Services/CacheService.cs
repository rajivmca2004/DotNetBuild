using DotNetBuild.Infrastructure.Repositories.Interfaces;
using DotNetBuild.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DotNetBuild.Services
{
   public class CacheService : ICacheService
    {
        protected readonly ICacheDataRepository _cacheDataRepository;
        public CacheService(ICacheDataRepository cacheDataRepository)
        {
            _cacheDataRepository = cacheDataRepository;
        }

        public async Task<string> Get(string key)
        {
            return await _cacheDataRepository.Get(key: key);
        }

        public async Task<string> Post(Dictionary<string, string> data)
        {
            return await _cacheDataRepository.Post(data: data);
        }

        public async Task<string> Put(Dictionary<string, string> data)
        {
            return await _cacheDataRepository.Put(data: data);
        }

        public async Task<string> Delete(string key)
        {
            return await _cacheDataRepository.Delete(key: key);
        }
    }
}
