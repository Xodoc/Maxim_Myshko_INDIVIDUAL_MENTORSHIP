using DAL.Entities.WeatherHistoryEntities;
using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Subscription
    {
        public int Id { get; set; }

        public string Cron { get; set; }

        public string UserId { get; set; }

        public bool IsActive { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? LastSendTime { get; set; }

        public User User { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
