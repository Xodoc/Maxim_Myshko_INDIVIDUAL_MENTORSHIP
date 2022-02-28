using System;

namespace DAL.Entities
{
    public class TemperatureInfo : ICloneable
    {
        public string CityName { get; set; }

        public double Temp { get; set;}

        public int SuccessfullRequest { get; set; }

        public int FailedRequest { get;set; }

        public long RunTime { get; set; }

        public int Canceled { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
