using System.Collections.Generic;

namespace DAL.Entities.WeatherHistoryEntities
{
    public class City
    {
        public int Id { get; set; }

        public string CityName { get; set; }

        public ICollection<WeatherHistory> WeatherHistories { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
