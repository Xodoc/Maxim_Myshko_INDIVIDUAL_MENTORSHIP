using BL.DTOs;
using BL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IValidator<Root> _validator;

        public WeatherService(IWeatherRepository weatherRepository, IValidator<Root> validator)
        {
            _validator = validator;
            _weatherRepository = weatherRepository;
        }

        public async Task<WeatherDTO> GetWeatherAsync(string cityName)
        {
            _validator.ValidateCityName(cityName);

            var weather = await _weatherRepository.GetWeatherAsync(cityName);

            SetWeatherDescription(weather);

            return MapToWeatherDTO(weather);
        }

        private void SetWeatherDescription(Root root)
        {
            _validator.Validate(root);

            var weather = root.weather.FirstOrDefault();

            if (root.main.temp < 0)
                weather.description = "Dress warmly.";

            if (root.main.temp >= 0 && root.main.temp <= 20)
                weather.description = "It's fresh.";

            if (root.main.temp >= 30 && root.main.temp <= 30)
                weather.description = "Good weather.";

            if (root.main.temp >= 30)
                weather.description = "it's time to go to the beach.";
        }

        private WeatherDTO MapToWeatherDTO(Root weather)
        {
            _validator.Validate(weather);

            var weatherNowDTO = new WeatherDTO
            {
                Name = weather.name,
                Description = weather.weather[0].description,
                Temp = weather.main.temp
            };

            return weatherNowDTO;
        }
    }
}
