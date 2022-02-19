namespace DAL.Entities
{
    public class TemperatureInfo
    {
        public string CityName { get; set; }

        public double Temp { get; set;}

        public int CountSuccessfullRequests { get; set; }

        public int CountFailedRequests { get;set; }

        public long RunTime { get; set; }
    }
}
