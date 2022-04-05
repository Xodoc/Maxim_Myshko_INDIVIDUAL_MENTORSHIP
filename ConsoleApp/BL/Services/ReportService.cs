using AutoMapper;
using BL.DTOs;
using BL.Interfaces;
using DAL.Entities.WeatherHistoryEntities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly ICityService _cityService;
        private readonly IWeatherHistoryRepository _weatherHistoryRepository;

        public ReportService(IMapper mapper, IWeatherHistoryRepository weatherHistoryRepository, ICityService cityService)
        {
            _mapper = mapper;
            _weatherHistoryRepository = weatherHistoryRepository;
            _cityService = cityService;
        }

        public async Task<string> CreateReportAsync(IEnumerable<string> cityNames, TimeSpan period)
        {
            var cities = _mapper.Map<List<City>>(await _cityService.GetCitiesByCityNamesAsync(cityNames));

            var histories = await _weatherHistoryRepository.GetWeatherHistoriesByCitiesAsync(cities, period);

            var validationResult = ValidateExistsStatisticsFromResponseModel(cities, histories);

            var tempDtos = _mapper.Map<List<TempDTO>>(histories);

            var averageTemps = GetAverageTemps(tempDtos);

            return CreateMessage(averageTemps, period, validationResult);
        }

        private IEnumerable<TempDTO> GetAverageTemps(List<TempDTO> temps)
        {
            // Надо взять id города, потом в цикле посчитать average для каждого города с определенным id
            var ids = temps.Select(x => x.City.Id).Distinct().OrderBy(x => x).ToList();

            var result = new List<TempDTO>();

            for (int i = 0; i < ids.Count; i++)
            {
                var specialTemp = temps.Where(x => x.City.Id == ids[i]);
                var cities = temps.Select(x => x.City).Distinct();

                var temp = new TempDTO
                {
                    AverageTemp = specialTemp.Average(x => x.Temp),
                    City = cities.FirstOrDefault(x => x.Id == ids[i])
                };
                result.Add(temp);
            }

            return result;
        }

        public IEnumerable<string> ValidateExistsStatisticsFromResponseModel(IEnumerable<City> cities, IEnumerable<WeatherHistory> histories)
        {
            var uniqueCityNames = cities.Select(x => x.CityName).Distinct();
            var uniqueCityNamesFromHistories = histories.Select(x => x.City.CityName).Distinct();

            var result = uniqueCityNames.Except(uniqueCityNamesFromHistories);

            if (result.Count() != 0)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        private string CreateMessage(IEnumerable<TempDTO> averageTemps, TimeSpan period, IEnumerable<string> validationResult)
        {
            var message = new StringBuilder();

            var from = DateTime.Now.Subtract(period);

            message.AppendLine($"The report was generated: {DateTime.Now}. Period: {from} - {DateTime.Now}");

            if (averageTemps.Count() == 0)
            {
                message.AppendLine("No statistics on cities.");
            }

            foreach (var temp in averageTemps)
            {
                message.AppendLine($"{temp.City.CityName} average temperature: {temp.AverageTemp} °C.");
            }

            if (validationResult.Count() > 0)
            {
                foreach (var cityName in validationResult) 
                {
                    message.AppendLine($"{cityName}: no statistics.");
                }
            }

            return message.ToString();
        }
    }
}
