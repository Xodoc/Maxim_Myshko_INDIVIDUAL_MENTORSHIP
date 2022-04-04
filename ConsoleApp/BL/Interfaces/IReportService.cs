using DAL.Entities.WeatherHistoryEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IReportService
    {
        Task<string> CreateReportAsync(IEnumerable<string> cities, TimeSpan time);
    }
}
