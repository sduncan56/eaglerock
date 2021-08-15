using EagleRock.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrafficController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        public TrafficController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]PayloadDto data)
        {
            var key = data.BotId;

            var json = JsonConvert.SerializeObject(data);
            var latestDataBytes = Encoding.UTF8.GetBytes(json);

            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddDays(10));

            await _distributedCache.SetAsync(key, latestDataBytes, options);

            DataFunctions.DroneDataFunctions.AddKey(key, _distributedCache);
           
        
            return Ok();

        }

        [HttpGet]
        public async Task<ActionResult<PayloadDto>> Get(string botId)
        {
            return await DataFunctions.DroneDataFunctions.GetData(botId, _distributedCache);
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayloadDto>>> GetAll()
        {
            List<PayloadDto> result = new List<PayloadDto>();

            var keys = await DataFunctions.DroneDataFunctions.GetKeys(_distributedCache);
            foreach (string key in keys)
            {
                result.Add(await DataFunctions.DroneDataFunctions.GetData(key, _distributedCache));
            }

            return result;
        }
    }
}
