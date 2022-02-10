using DAL.Entities;
using DAL.Entities.WeatherForecastEntities;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IWeatherRepository
    {
        Task<Root> GetWeatherAsync(string cityName);

        Task<WeatherForecast> GetWeatherForecastAsync(string cityName, int days);
    }
}
