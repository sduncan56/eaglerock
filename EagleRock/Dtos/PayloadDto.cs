using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleRock.Dtos
{
    public class PayloadDto
    {
        public string BotId { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime Timestamp { get; set; }
        public string Road { get; set; }
        //public int TrafficDirection { get; set; }
        //public float CarsPerMinute { get; set; }
        //public 


        public override bool Equals(object obj)
        {
            var toCompare = obj as PayloadDto;
            if (toCompare == null)
                return false;
            return BotId == toCompare.BotId
                && Longitude == toCompare.Longitude
                && Latitude == toCompare.Latitude
                && Timestamp == toCompare.Timestamp
                && Road == toCompare.Road;
        }
    }
}
