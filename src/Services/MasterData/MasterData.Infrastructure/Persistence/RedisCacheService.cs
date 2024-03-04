using MasterData.Domain.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MasterData.Infrastructure.Persistence
{
    public class RedisCacheService : ICacheService
    {
        private IDatabase _redisDb;
        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _redisDb = connectionMultiplexer.GetDatabase();

        }

        public T? GetData<T>(string key)
        {
            var value = _redisDb.StringGet(key);
            if (value.IsNullOrEmpty)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(value.ToString());            
        }

        public Task<T?> GetDataAsync<T>(string key)
        {
            return _redisDb.StringGetAsync(key).ContinueWith((result) =>
            {
                var value = result.Result;
                if (value.IsNullOrEmpty)
                {
                    return default;
                }
                return JsonConvert.DeserializeObject<T>(value.ToString());
            });
            
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _redisDb.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }

        public Task<bool> SetDataAsync<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            return _redisDb.StringSetAsync(key, JsonConvert.SerializeObject(value), expiryTime);
        }

        public object RemoveData(string key)
        {
            bool _isKeyExist = _redisDb.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _redisDb.KeyDelete(key);
            }
            return false;
        }

        public async Task<object> RemoveDataAsync(string key)
        {
            bool _isKeyExist = await _redisDb.KeyExistsAsync(key);
            if (_isKeyExist == true)
            {
                return await _redisDb.KeyDeleteAsync(key);
            }
            return false;
        }
    }
}
