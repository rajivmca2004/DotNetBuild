using Microsoft.Extensions.Caching.Distributed;
using DotNetBuild.Infrastructure.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DotNetBuild.Infrastructure.Repositories
{
      public class RedisCacheDataRepository : ICacheDataRepository
        {
            private readonly IDistributedCache _cache;
            public RedisCacheDataRepository(IDistributedCache cache)
            {
                _cache = cache;
            }

            public async Task<string> Get(string key = "")
            {
                if (key != null)
                {
                    return await _cache.GetStringAsync(key);
                }
                else
                {
                    return $"Key cann't be null.";
                }
            }

            public async Task<string> Post(Dictionary<string, string> data)
            {
                try
                {
                    foreach (var item in data)
                    {
                        await _cache.SetStringAsync(item.Key, item.Value);
                    }
                    return $"Data Inserted Succesfully !!";
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
            }

            public async Task<string> Put(Dictionary<string, string> data)
            {
                try
                {
                    foreach (var item in data)
                    {
                        //await _cache.PutStringAsync(item.Key, item.Value);
                    }
                    return $"Data Updated Succesfully !!";
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
            }

            public async Task<string> Delete(string key)
            {
                try
                {
                    if (key != null)
                    {
                        await _cache.RemoveAsync(key);
                        return $"Data Deleted Succesfully with ID: {key}";
                    }
                    else
                    {
                        return $"Key cann't be null.";
                    }
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
            }
        }
}
