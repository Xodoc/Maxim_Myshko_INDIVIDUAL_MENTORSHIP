using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ReportRequest
    {
        [Required(ErrorMessage = "SubscriptionId is required")]
        public int SubscriptionId { get; set; }

        [Required(ErrorMessage = "FromDate is required")]
        public DateTime FromDate { get; set; }
    }
}
