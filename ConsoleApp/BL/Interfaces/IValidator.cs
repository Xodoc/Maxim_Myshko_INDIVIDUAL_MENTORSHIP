using DAL.Entities.WeatherHistoryEntities;
using System.Collections.Generic;

namespace BL.Interfaces
{
    public interface IValidator
    {
        void ValidateCityName(string value);

        void ValidateModel(string cityName, int days);

        void ValidateCityNames(IEnumerable<string> cityNames);

        void ValidateWeatherHistories(WeatherHistory history);

        void ValidateConfigNames(IEnumerable<string> names);
    }
}
