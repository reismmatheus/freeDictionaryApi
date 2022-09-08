using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace FreeDictionary.Service.RedisCache
{
    public class RedisCacheClient : IRedisCacheClient
    {
        private readonly IDistributedCache _distributedCache;
        public RedisCacheClient(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var stringResult = await _distributedCache.GetStringAsync(key);
            if (stringResult == null) return default(T);
            return JsonConvert.DeserializeObject<T>(stringResult);
        }

        public async Task SetAsync(string key, object value, bool serialize = false)
        {
            var serializeString = value.ToString();
            if (serialize) serializeString = JsonConvert.SerializeObject(value);
            await _distributedCache.SetStringAsync(key, serializeString);
        }
    }
}
