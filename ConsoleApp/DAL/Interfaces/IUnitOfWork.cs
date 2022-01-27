using Shared.Interfaces;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IWeatherRepository WeatherRepository { get; }

        IConfiguration Configuration { get; }
    }
}
