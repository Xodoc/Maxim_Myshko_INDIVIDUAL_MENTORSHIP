﻿using DAL.Entities;
using DAL.Entities.WeatherForecastEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IWeatherRepository
    {
        Task<CurrentWeather> GetWeatherAsync(string cityName);

        Task<WeatherForecast> GetWeatherForecastAsync(string cityName, int days);

        List<MaxTemperature> GetTemperatures(List<string> cityNames);
    }
}
