using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherHistoryController : ControllerBase
    {
        private readonly IWeatherHistoryService _weatherHistoryService;

        public WeatherHistoryController(IWeatherHistoryService weatherHistoryService)
        {
            _weatherHistoryService = weatherHistoryService;
        }

        [HttpGet("getWeatherHistory")]
        public async Task<IActionResult> GetWeatherHistory() 
        {
            return Ok(await _weatherHistoryService.AddWeatherHistoryAsync());
        }
    }
}
