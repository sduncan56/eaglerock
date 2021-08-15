using EagleRock.Utilities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.DataFunctions
{
    public class DroneKeys
    {
        public List<string> Keys { get; set; }
    }
    public class DroneDataFunctions
    {
        public static async Task<List<string>> GetKeys(IDistributedCache distributedCache)
        {
            var savedData = await distributedCache.GetAsync("keys");
            return savedData != null ? JsonConvert.DeserializeObject<List<string>>(Encoding.UTF8.GetString(savedData)) : new List<string>();

        }

        public static async void AddKey(string key, IDistributedCache cache)
        {
            var keys = await GetKeys(cache);

            if (!keys.Any(x=>x == key))
                keys.Add(key);

            var bytes = DataStoreHelpers.SerialiseObject(keys);

            var options = new DistributedCacheEntryOptions()
                 .SetAbsoluteExpiration(DateTime.Now.AddDays(10));
            await cache.SetAsync("keys", bytes, options);
        }

    }
}
