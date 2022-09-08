using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Service.RedisCache
{
    public interface IRedisCacheClient
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync(string key, object value, bool serialize = false);
    }
}
