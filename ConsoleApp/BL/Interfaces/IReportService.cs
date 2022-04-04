using DAL.Entities.WeatherHistoryEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IReportService
    {
        Task<string> CreateReportAsync(IEnumerable<City> cities, TimeSpan time);

        List<City> GetCities();
    }
}
