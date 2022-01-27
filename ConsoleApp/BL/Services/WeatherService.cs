using BL.DTOs;
using BL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Threading.Tasks;

namespace BL.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Root> _validator;

        public WeatherService(IUnitOfWork unitOfWork, IValidator<Root> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<WeatherNowDTO> GetWeatherAsync(string cityName)
        {
            var weather = await _unitOfWork.WeatherRepository.GetWeatherAsync(cityName);

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
