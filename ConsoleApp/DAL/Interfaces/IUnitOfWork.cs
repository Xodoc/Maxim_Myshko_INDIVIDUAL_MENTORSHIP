using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IWeatherRepository WeatherRepository { get; }
    }
}
