using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IWeatherRepository
    {
        Task<Root> GetWeatherAsync(string cityName);
    }
}
