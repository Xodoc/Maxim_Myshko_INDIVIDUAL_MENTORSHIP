using BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherHistoryController : ControllerBase
    {
        private readonly IWeatherHistoryService _weatherHistoryService;

        public WeatherHistoryController(IWeatherHistoryService weatherHistoryService)
        {
            _weatherHistoryService = weatherHistoryService;
        }

        /// <summary>
        /// Getting weather history by city name and time interval
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("getWeatherHistory")]
        public async Task<IActionResult> GetWeatherHistory([FromQuery] WeatherHistoryRequest request)
        {
            return Ok(await _weatherHistoryService.GetWeatherHistoriesAsync(request.CityName, request.From, request.To));
        }
    }
}
