using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleRock.Utilities
{
    public static class DataStoreHelpers
    {
        public static byte[] SerialiseObject(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(json);
        }

    }
}
