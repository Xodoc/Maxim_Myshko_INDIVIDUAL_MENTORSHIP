using BL.DTOs;
using BL.Interfaces;
using BL.Validators.CustomExceptions;
using DAL.Entities;
using DAL.Entities.WeatherForecastEntities;
using DAL.Interfaces;
using Serilog;
using Shared.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IValidator _validator;
        private readonly IConfiguration _config;
        private CancellationTokenSource _cts;

        public WeatherService(IWeatherRepository weatherRepository, IValidator validator, IConfiguration config)
        {
            _validator = validator;
            _weatherRepository = weatherRepository;
            _config = config;
        }

        public async Task<string> GetWeatherAsync(string cityName)
        {
            Log.Information("Method GetWeatherAsync has been run!");

            _validator.ValidateCityName(cityName);

            _cts = new CancellationTokenSource();

            var weather = await _weatherRepository.GetWeatherAsync(cityName, _cts.Token);

            weather = SetWeatherDescription(weather);

            Log.Information("Method GetWeatherAsync is complited!");

            return $"\nIn {weather.Name} {weather.Main.Temp}°C now. {weather.Weather.First().Description}\n";
        }

        public async Task<string> GetWeatherForecastAsync(string cityName, int days)
        {
            Log.Information("Method GetWeatherForecastAsync has been run!");

            _validator.ValidateModel(cityName, days);

            var weatherForecast = await _weatherRepository.GetWeatherForecastAsync(cityName, days);

            var weatherForecastDtos = MapToWeatherForecastDTOs(weatherForecast);

            var responseMessage = "";
            var numberOfDay = 1;

            weatherForecastDtos.ForEach(x => responseMessage +=
                $"{x.CityName} weather forecast:\nDay {numberOfDay++}: {x.Temp}°C. {x.Description}\n");

            Log.Information("Method GetWeatherForecastAsync is complited!");

            return responseMessage;
        }

        public async Task<string> GetMaxTemperatureAsync(IEnumerable<string> cityNames)
        {
            Log.Information("Method GetMaxTemperatureAsync has been run!");

            _validator.ValidateCityNames(cityNames);
            _cts = new CancellationTokenSource();
            _cts.CancelAfter(_config.MaxWaitingTime);

            var maxTemps = await _weatherRepository.GetTemperaturesAsync(cityNames, _cts.Token);

            var maxTemp = CalculateTotalsForMessage(maxTemps);

            var responseMessage = new StringBuilder();

            if (_config.IsDebug == true)
            {
                responseMessage.Append("[Debug]\n");

                foreach (var temp in maxTemps)
                {
                    if (temp.FailedRequest > 0 && temp.Canceled == 0)
                        responseMessage.Append($"City: {temp.CityName}. Error: Invalid city name. Timer: {temp.RunTime} ms.\n");
                    else if (temp.Canceled == 0)
                        responseMessage.Append($"City: {temp.CityName}. Temperature: {temp.Temp}°C. Timer: {temp.RunTime} ms.\n");
                    else if (temp.Canceled > 0)
                        responseMessage.Append($"Weather request for {temp.CityName} was canceled due to a timeout.\n");
                }
            }

            if (maxTemp.SuccessfullRequest > 0)
                responseMessage.AppendLine(
@$"City with the highest temperature {maxTemp.Temp}°C: {maxTemp.CityName}.
Successful request count: {maxTemp.SuccessfullRequest}, failed: {maxTemp.FailedRequest}, canceled: {maxTemp.Canceled}.");
            else
                responseMessage.AppendLine($"No successful requests. Failed requests count: {maxTemp.FailedRequest}, canceled: {maxTemp.Canceled}.");

            Log.Information("Method GetMaxTemperatureAsync is complited!");

            return responseMessage.ToString();
        }

        private TemperatureInfo CalculateTotalsForMessage(IEnumerable<TemperatureInfo> temps)
        {
            var successfullRequests = temps.Select(x => x.SuccessfullRequest).Sum();
            var failedRequests = temps.Select(x => x.FailedRequest).Sum();
            var canceled = temps.Select(x => x.Canceled).Sum();
            var maxTemp = temps.FirstOrDefault(x => x.Temp == temps.Max(e => e.Temp));

            var result = (TemperatureInfo)maxTemp.Clone();
            result.FailedRequest = failedRequests;
            result.SuccessfullRequest = successfullRequests;
            result.Canceled = canceled;

            return result;
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

        private List<WeatherForecastDTO> SetWeatherForecastDescription(IList<WeatherForecastDTO> weatherForecastDto)
        {
            if (weatherForecastDto == null)
                throw new ValidatorException("\nInvalid data entered");

            for (int i = 0; i < weatherForecastDto.Count; i++)
            {
                weatherForecastDto[i].Description = SetDescription(weatherForecastDto[i].Temp);
            }

            return weatherForecastDto.ToList();
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
