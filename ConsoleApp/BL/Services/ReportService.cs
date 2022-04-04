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
        private readonly IWeatherHistoryRepository _weatherHistoryRepository;

        public ReportService(IMapper mapper, IWeatherHistoryRepository weatherHistoryRepository)
        {
            _mapper = mapper;
            _weatherHistoryRepository = weatherHistoryRepository;
        }

        public async Task<string> CreateReportAsync(IEnumerable<City> cities, TimeSpan period)
        {
            var history = await _weatherHistoryRepository.GetWeatherHistoriesByCitiesAsync(cities, period);

            var tempDtos = _mapper.Map<List<TempDTO>>(history);

            var averageTemps = GetAverageTemps(tempDtos);

            return CreateMessage(averageTemps, period);
        }

        public List<City> GetCities()
        {
            var cities = new List<City>
            {
                new City{Id = 1, CityName = "Minsk" },
                new City{Id = 2, CityName = "London" },
                new City{Id = 3, CityName = "Moscow" }
            };

            return cities;
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

        private string CreateMessage(IEnumerable<TempDTO> averageTemps, TimeSpan period)
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
                if (temp == null)
                {
                    message.AppendLine($"{temp.City.CityName}: no statistics.");
                }
                else
                {
                    message.AppendLine($"{temp.City.CityName} average temperature: {temp.AverageTemp} °C.");
                }
            }

            return message.ToString();
        }
    }
}
