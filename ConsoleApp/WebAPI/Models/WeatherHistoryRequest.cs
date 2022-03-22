using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class WeatherHistoryRequest : WeatherBaseRequest
    {
        [Required(ErrorMessage = "From is required")]
        public DateTime From { get; set; }
        
        [Required(ErrorMessage = "To is required")]       
        public DateTime To { get; set; }
    }
}
