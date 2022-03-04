using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebAPI.Models
{
    public class WeatherHistoryRequest : WeatherBaseRequest
    {
        [Required(ErrorMessage = "Date is required")]
        public string Date { get; set; }
    }
}
