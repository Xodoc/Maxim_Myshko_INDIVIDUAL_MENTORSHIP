using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IWeatherService
    {
        Task<string> GetWeatherAsync(string cityName);
    }
}
