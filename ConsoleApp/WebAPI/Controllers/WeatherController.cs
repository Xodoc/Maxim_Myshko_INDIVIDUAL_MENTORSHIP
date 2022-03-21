using BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        /// <summary>
        /// Getting current weather by city name
        /// </summary>
        /// <param name="weather"></param>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="400">If send data is incorrect</response>
        [HttpGet("getCurrentWeather")]
        public async Task<IActionResult> GetCurrentWeather([FromQuery] WeatherBaseRequest weather)
        {
            return Ok(await _weatherService.GetWeatherAsync(weather.CityName));
        }

        [HttpGet("getWeatherForecast")]
        public async Task<IActionResult> GetWeatherForecast([FromQuery] WeatherForecastRequest weather)
        {
            return Ok(await _weatherService.GetWeatherForecastAsync(weather.CityName, weather.Days));
        }
    }
}
