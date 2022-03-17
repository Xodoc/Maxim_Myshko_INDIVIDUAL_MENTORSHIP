using AutoMapper;
using BL.DTOs;
using BL.EqualityComparers;
using BL.Interfaces;
using DAL.Database;
using DAL.Entities.WeatherHistoryEntities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntegrationTests.Services
{
    public class WeatherHistoryServiceIntegrationTest : IDisposable
    {
        private readonly IMapper _mapper;
        private readonly IWeatherHistoryService _weatherHistoryService;
        private readonly IDbContextTransaction _transaction;
        private readonly ApplicationDbContext _context;

        //Setup
        public WeatherHistoryServiceIntegrationTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddDI().AddLogging().BuildServiceProvider();

            _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetService<IMapper>();
            _weatherHistoryService = serviceProvider.GetService<IWeatherHistoryService>();

            _transaction = _context.Database.BeginTransaction();
        }

        //Teardown
        public async void Dispose()
        {
            await _transaction.RollbackAsync();

            GC.SuppressFinalize(this);
        }

        [Theory]
        [InlineData("Minsk", "12.03.2022", "16.03.2022")]
        public async void GetWeatherHistoryAsync_WhenSendingCorrectData_GettingWeatherHistory(string cityName, string from, string to)
        {
            //Arrange
            var expected = new WeatherHistoryDTO { CityId = 1, Timestamp = DateTime.Parse(from), Temp = -2.14 };
            var data = _mapper.Map<WeatherHistory>(expected);

            //Act
            await _context.WeatherHistories.AddAsync(data);
            await _context.SaveChangesAsync();

            var actualResult = await _weatherHistoryService.GetWeatherHistoriesAsync(cityName, DateTime.Parse(from), DateTime.Parse(to));

            //Assert
            Assert.Equal(expected, actualResult[0], new WeatherHistoryDTOComparator());
        }

        [Theory]
        [InlineData("Minsk", "12.03.2022", "16.03.2022")]
        public async void GetWeatherHistoryAsync_WhenSendingIncorrectData_GettingWeatherHistory(string cityName, string from, string to)
        {
            //Arrange
            var expectedList = new List<WeatherHistoryDTO>();
            var someData = new WeatherHistory { CityId = 1, Timestamp = DateTime.Parse("11.03.2022"), Temp = -2.14 };

            //Act
            await _context.WeatherHistories.AddAsync(someData);
            await _context.SaveChangesAsync();

            var actualResult = await _weatherHistoryService.GetWeatherHistoriesAsync(cityName, DateTime.Parse(from), DateTime.Parse(to));

            //Asssert
            Assert.Equal(expectedList, actualResult, new WeatherHistoryDTOComparator());
        }
    }
}
