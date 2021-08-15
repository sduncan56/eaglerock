using EagleRock.Controllers;
using EagleRock.Dtos;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EagleRock.Test.Controllers
{
    public class TrafficControllerTest
    {
        PayloadDto botData1;
        PayloadDto botData2;

        IDistributedCache cache;

        public TrafficControllerTest()
        {
            botData1 = new PayloadDto()
            {
                BotId = "939c907e-3f3c-406f-a5c8-e071d54ca5e7",
                Longitude = 153.025131M,
                Latitude = -27.469770M,
                Timestamp = DateTime.MinValue,
                Road = "Queen Street"
            };

            botData2 = new PayloadDto()
            {
                BotId = "343165c4-7cfd-458c-8fd4-1a26242043e4",
                Longitude = 153.006653M,
                Latitude = -27.640989M,
                Timestamp = DateTime.MinValue,
                Road = "Logan Motorway"
            };

            var opts = Options.Create<MemoryDistributedCacheOptions>(new MemoryDistributedCacheOptions());
            cache = new MemoryDistributedCache(opts);
        }

        [Fact]
        public async void CreatesNewDroneDataEntry()
        {
            TrafficController controller = new TrafficController(cache);

            await controller.Create(botData1);

            var savedData = await cache.GetAsync(botData1.BotId);
            PayloadDto result = JsonConvert.DeserializeObject<PayloadDto>(Encoding.UTF8.GetString(savedData));
            Assert.Equal(botData1, result);
        }

    }
}
