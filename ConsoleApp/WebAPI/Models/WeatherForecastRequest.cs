using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class WeatherForecastRequest : WeatherBaseRequest
    {
        [Required(ErrorMessage = "Days is required")]
        public int Days { get; set; }
    }
}
