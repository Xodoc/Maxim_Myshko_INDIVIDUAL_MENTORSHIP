using DAL.Entities;
using DAL.Entities.WeatherForecastEntities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IWeatherRepository
    {
        Task<CurrentWeather> GetWeatherAsync(string cityName, CancellationToken ct);

        Task<WeatherForecast> GetWeatherForecastAsync(string cityName, int days);

        Task<List<TemperatureInfo>> GetTemperaturesAsync(IEnumerable<string> cityNames, CancellationToken ct);
    }
}
