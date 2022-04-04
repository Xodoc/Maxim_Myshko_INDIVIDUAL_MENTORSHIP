using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebAPI.Models
{
    public class ReportRequest
    {
        [Required(ErrorMessage = "CityNames is required")]
        public string[] CityNames { get; set; }

        [Required(ErrorMessage = "FromDate is required")]
        public DateTime FromDate { get; set; }
    }
}
