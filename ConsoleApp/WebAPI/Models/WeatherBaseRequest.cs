using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebAPI.Models
{
    public class WeatherBaseRequest
    {
        [Required(ErrorMessage = "City name is required")]
        public string CityName { get; set; }
    }
}
