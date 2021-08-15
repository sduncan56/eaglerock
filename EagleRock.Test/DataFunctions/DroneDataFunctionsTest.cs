using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using EagleRock.DataFunctions;

namespace EagleRock.Test.DataFunctions
{
    public class DroneDataFunctionsTest
    {
        IDistributedCache cache;

        public DroneDataFunctionsTest()
        {
            var opts = Options.Create<MemoryDistributedCacheOptions>(new MemoryDistributedCacheOptions());
            cache = new MemoryDistributedCache(opts);
        }

        [Fact]
        public async void AddNewKey()
        {
            var key = "9772cc10-3a5b-4d83-b1aa-02bd998027c7";
            DroneDataFunctions.AddKey(key, cache);

            var keys = await DroneDataFunctions.GetKeys(cache);
            Assert.True(keys.SingleOrDefault(x => x == key) != null);

            //adding same key doesn't add duplicate
            DroneDataFunctions.AddKey(key, cache);
            keys = await DroneDataFunctions.GetKeys(cache);
            Assert.True(keys.SingleOrDefault(x => x == key) != null);

            //add second key
            var key2 = "98f88aee-b825-486f-8591-b6cc1e9a2c5a";
            DroneDataFunctions.AddKey(key2, cache);
            keys = await DroneDataFunctions.GetKeys(cache);
            Assert.True(keys.SingleOrDefault(x => x == key) != null);
            Assert.True(keys.SingleOrDefault(x => x == key2) != null);




        }
    }
}
