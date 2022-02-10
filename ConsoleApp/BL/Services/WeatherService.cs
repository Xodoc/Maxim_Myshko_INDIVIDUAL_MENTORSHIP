using BL.DTOs;
using BL.Interfaces;
using BL.Validators.CustomExceptions;
using DAL.Entities;
using DAL.Entities.WeatherForecastEntities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IValidator _validator;

        public WeatherService(IWeatherRepository weatherRepository, IValidator validator)
        {
            _validator = validator;
            _weatherRepository = weatherRepository;
        }

        public async Task<string> GetWeatherAsync(string cityName)
        {
            _validator.ValidateCityName(cityName);

            var weather = await _weatherRepository.GetWeatherAsync(cityName);

            weather = SetWeatherDescription(weather);

            return $"\nIn {weather.Name} {weather.Main.Temp}°C now. {weather.Weather.First().Description}\n";
        }

        public async Task<string> GetWeatherForecastAsync(string cityName, int days)
        {
            _validator.ValidateModel(cityName, days);

            var weatherForecast = await _weatherRepository.GetWeatherForecastAsync(cityName, days);

            var weatherForecastDtos = MapToWeatherForecastDTOs(weatherForecast);

            weatherForecastDtos = SetWeatherForecastDescription(weatherForecastDtos);

            var responseMessage = "";

            foreach (var item in weatherForecastDtos) 
            {
                responseMessage += $"{item.CityName} weather forecast: {item.Temp}°C. {item.Description}\n";
            }

            return responseMessage;
        }

        private Root SetWeatherDescription(Root root)
        {
            if (root == null)
                throw new ValidatorException("\nInvalid data entered");


            var weather = root.Weather.FirstOrDefault();

            if (root.Main.Temp < 0)
                weather.Description = "Dress warmly.";

            if (root.Main.Temp >= 0 && root.Main.Temp <= 20)
                weather.Description = "It's fresh.";

            if (root.Main.Temp >= 20 && root.Main.Temp <= 30)
                weather.Description = "Good weather.";

            if (root.Main.Temp >= 30)
                weather.Description = "It's time to go to the beach.";

            return root;
        }

        private List<WeatherForecastDTO> SetWeatherForecastDescription(List<WeatherForecastDTO> weatherForecastDto)
        {
            if (weatherForecastDto == null)
                throw new ValidatorException("Invalid data entered");

            for (int i = 0; i < weatherForecastDto.Count; i++)
            {
                if (weatherForecastDto[i].Temp < 0)
                    weatherForecastDto[i].Description = "Dress warmly.";

                if (weatherForecastDto[i].Temp >= 0 && weatherForecastDto[i].Temp <= 20)
                    weatherForecastDto[i].Description = "It's fresh.";

                if (weatherForecastDto[i].Temp >= 20 && weatherForecastDto[i].Temp <= 30)
                    weatherForecastDto[i].Description = "Good weather.";

                if (weatherForecastDto[i].Temp >= 30)
                    weatherForecastDto[i].Description = "It's time to go to the beach."; 
            }

            return weatherForecastDto;
        }

        private List<WeatherForecastDTO> MapToWeatherForecastDTOs(WeatherForecast weatherForecast)
        {
            if (weatherForecast == null)
                throw new ValidatorException("Invalid data entered");

            var weatherForecastDtos = new List<WeatherForecastDTO>();

            for (int i = 0; i < weatherForecast.Daily.Count; i++)
            {
                var weatherForecastDto = new WeatherForecastDTO
                {
                    CityName = weatherForecast.CityName,
                    Temp = weatherForecast.Daily[i].Temp.day,
                    Description = weatherForecast.Daily[i].Weather[0].description
                };

                weatherForecastDtos.Add(weatherForecastDto);
            }

            return weatherForecastDtos;
        }
    }
}
