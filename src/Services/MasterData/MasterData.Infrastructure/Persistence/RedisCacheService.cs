using MasterData.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace MasterData.Infrastructure.Persistence
{
    public class RedisCacheService(IDistributedCache redisDb) : ICacheService
    {
        public T? GetData<T>(string key)
        {
            var value = redisDb.GetString(key);
            if (value.IsNullOrEmpty())
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(value.ToString());            
        }

        public Task<T?> GetDataAsync<T>(string key)
        {
            return redisDb.GetStringAsync(key).ContinueWith((result) =>
            {
                var value = result.Result;
                if (value.IsNullOrEmpty())
                {
                    return default;
                }
                return JsonConvert.DeserializeObject<T>(value.ToString());
            });
            
        }

        public void SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // Set expiration time
            };
            redisDb.SetString(key, JsonConvert.SerializeObject(value), options);            
        }

        public Task SetDataAsync<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // Set expiration time
            };
            return redisDb.SetStringAsync(key, JsonConvert.SerializeObject(value), options);
        }

        public void RemoveData(string key)
        {
            var _isKeyExist = redisDb.GetString(key) != null;
            if (_isKeyExist == true)
            {
                redisDb.Remove(key);
            }            
        }

        public async Task RemoveDataAsync(string key)
        {
            var _isKeyExist = redisDb.GetString(key);
            if (_isKeyExist != null)
            {
                await redisDb.RemoveAsync(key);
            }
        }
    }
}
