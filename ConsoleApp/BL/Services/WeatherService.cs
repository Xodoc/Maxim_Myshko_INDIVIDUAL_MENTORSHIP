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

            var responseMessage = "";
            var numberOfDay = 1;

            weatherForecastDtos.ForEach(x => responseMessage +=
                $"{x.CityName} weather forecast:\nDay {numberOfDay++}: {x.Temp}°C. {x.Description}\n");

            return responseMessage;
        }

        public Task<string> GetMaxTemperatureAsync(List<string> cityNames)
        {
            _validator.ValidateCityNames(cityNames);

            var maxTemps = _weatherRepository.GetTemperatures(cityNames);
            var maxTemp = CalculateTotalsForMessage(maxTemps);

            string responseMessage = $"City with the highest temperature {maxTemp.Temp}°C: {maxTemp.CityName}." +
                  $" Successful request count: {maxTemp.CountSuccessfullRequests}, failed: {maxTemp.CountFailedRequests}.";

            //if (debugInfo == true)
            //{
            //    responseMessage = $"[Debug]\nCity: {maxTemp.CityName}. Temperature: {maxTemp.Temp}°C. Timer: {maxTemp.RunTime} ms.";
            //}

            return Task.FromResult(responseMessage);
        }

        private TemperatureInfo CalculateTotalsForMessage(List<TemperatureInfo> temps)
        {
            var successfullRequests = temps.Select(x => x.CountSuccessfullRequests).Sum();
            var failedRequests = temps.Select(x => x.CountFailedRequests).Sum();
            var maxTemp = temps.FirstOrDefault(x => x.Temp == temps.Max(e => e.Temp));

            maxTemp.CountFailedRequests = failedRequests;
            maxTemp.CountSuccessfullRequests = successfullRequests;

            return maxTemp;
        }

        private string SetDescription(double temp)
        {
            var description = "";

            if (temp < 0)
                description = "Dress warmly.";

            if (temp >= 0 && temp <= 20)
                description = "It's fresh.";

            if (temp >= 20 && temp <= 30)
                description = "Good weather.";

            if (temp >= 30)
                description = "It's time to go to the beach.";

            return description;
        }

        private CurrentWeather SetWeatherDescription(CurrentWeather root)
        {
            if (root == null)
                throw new ValidatorException("\nInvalid data entered");

            var weather = root.Weather.FirstOrDefault();

            weather.Description = SetDescription(root.Main.Temp);

            return root;
        }

        private List<WeatherForecastDTO> SetWeatherForecastDescription(List<WeatherForecastDTO> weatherForecastDto)
        {
            if (weatherForecastDto == null)
                throw new ValidatorException("\nInvalid data entered");

            for (int i = 0; i < weatherForecastDto.Count; i++)
            {
                weatherForecastDto[i].Description = SetDescription(weatherForecastDto[i].Temp);
            }

            return weatherForecastDto;
        }

        private List<WeatherForecastDTO> MapToWeatherForecastDTOs(WeatherForecast weatherForecast)
        {
            if (weatherForecast == null)
                throw new ValidatorException("\nInvalid data entered");

            var weatherForecastDtos = new List<WeatherForecastDTO>();

            for (int i = 0; i < weatherForecast.Daily.Count; i++)
            {
                var weatherForecastDto = new WeatherForecastDTO
                {
                    CityName = weatherForecast.CityName,
                    Temp = weatherForecast.Daily[i].Main.Temp,
                    Description = weatherForecast.Daily[i].Weather[0].Description,
                    Date = weatherForecast.Daily[i].Date
                };

                weatherForecastDtos.Add(weatherForecastDto);
            }

            return SetWeatherForecastDescription(weatherForecastDtos);
        }
    }
}
