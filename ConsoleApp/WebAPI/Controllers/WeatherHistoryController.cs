using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

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
        public async Task<IActionResult> GetWeatherHistory([FromQuery] WeatherHistoryRequest request)
        {
            return Ok(await _weatherHistoryService.GetWeatherHistoriesAsync(request.CityName, request.Date));
        }
    }
}
