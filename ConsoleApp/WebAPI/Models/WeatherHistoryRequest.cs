using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebAPI.Models
{
    public class WeatherHistoryRequest : WeatherBaseRequest
    {
        [Required(ErrorMessage = "From is required")]
        public string From { get; set; }
        
        [Required(ErrorMessage = "To is required")]       
        public string To { get; set; }
    }
}
