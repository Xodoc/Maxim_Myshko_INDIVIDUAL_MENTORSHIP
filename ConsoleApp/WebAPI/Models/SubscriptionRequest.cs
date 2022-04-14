using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebAPI.Models
{
    public class SubscriptionRequest
    {
        [Required(ErrorMessage = "Periodicity is required")]
        public int Periodicity { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "IsActive is required")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "FromDate is required")]
        public DateTime FromDate { get; set; }
    }
}
