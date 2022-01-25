using BL.DTOs;
using BL.Interfaces;
using BL.Validators;
using DAL.Entities;
using DAL.Interfaces;
using System.Threading.Tasks;

namespace BL.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private WeatherValidator _validator;

        public WeatherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<WeatherNowDTO> GetWeatherAndParseAsync(string cityName)
        {
            var weather = await _unitOfWork.WeatherRepository.GetWeatherAndParseAsync(cityName);

            _validator = new WeatherValidator();
            _validator.ValidateIfEntityExist(weather);

            weather.weather[0].description = GetWeaatherDescription(weather);

            return Mapping(weather);
        }

        private string GetWeaatherDescription(Root weather)
        {
            if (weather.main.temp < 0)
                return "Dress warmly.";
            if (weather.main.temp >= 0 && weather.main.temp <= 20)
                return "It's fresh.";
            if (weather.main.temp >= 30 && weather.main.temp <= 30)
                return "Good weather.";
            if (weather.main.temp >= 30)
                return "it's time to go to the beach.";

            return weather.weather[0].description;
        }

        private WeatherNowDTO Mapping(Root weather)
        {
            var weatherNowDTO = new WeatherNowDTO
            {
                Name = weather.name,
                Description = weather.weather[0].description,
                Temp = weather.main.temp
            };

            return weatherNowDTO;
        }
    }
}
