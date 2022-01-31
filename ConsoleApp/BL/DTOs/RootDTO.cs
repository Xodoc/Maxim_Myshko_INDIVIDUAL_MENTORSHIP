using System.Collections.Generic;

namespace BL.DTOs
{
    public class RootDTO
    {
        public CoordDTO coord { get; set; }

        public List<WeatherDTO> weather { get; set; }

        public string @base { get; set; }

        public MainDTO main { get; set; }

        public int visibility { get; set; }

        public WindDTO wind { get; set; }

        public CloudsDTO clouds { get; set; }

        public int dt { get; set; }

        public SysDTO sys { get; set; }

        public int timezone { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        public int cod { get; set; }
    }
}
