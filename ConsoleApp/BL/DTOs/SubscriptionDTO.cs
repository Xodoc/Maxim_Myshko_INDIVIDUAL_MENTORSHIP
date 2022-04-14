using System;

#nullable disable

namespace BL.DTOs
{
    public class SubscriptionDTO
    {
        public int Id { get; set; }

        public string Cron { get; set; }

        public string UserId { get; set; }

        public bool IsActive { get; set; }

        public DateTime FromDate { get; set; }
    }
}
