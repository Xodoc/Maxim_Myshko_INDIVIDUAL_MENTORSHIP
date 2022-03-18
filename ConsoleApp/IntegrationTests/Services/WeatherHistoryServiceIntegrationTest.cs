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
using System.Threading;
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
            var fixture = new List<WeatherHistoryDTO>
            {
                new WeatherHistoryDTO { CityId = 1, Timestamp = DateTime.Parse(from), Temp = -2.14 },
                new WeatherHistoryDTO { CityId = 1, Timestamp = DateTime.Parse("13.03.2022"), Temp = -2.14 },
                new WeatherHistoryDTO { CityId = 1, Timestamp = DateTime.Parse("11.03.2022"), Temp = 23 },
                new WeatherHistoryDTO { CityId = 1, Timestamp = DateTime.Parse(from), Temp = -15.1 }
            };
            var data = _mapper.Map<List<WeatherHistory>>(fixture);
            var expected = fixture.FindAll(x => x.Timestamp >= DateTime.Parse(from) && x.Timestamp <= DateTime.Parse(to));
            await _context.WeatherHistories.AddRangeAsync(data);
            await _context.SaveChangesAsync();

            //Act
            var actualResult = await _weatherHistoryService.GetWeatherHistoriesAsync(cityName, DateTime.Parse(from), DateTime.Parse(to));

            //Assert
            Assert.Equal(expected, actualResult, new WeatherHistoryDTOComparator());
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

        [Theory]
        [InlineData(1, "Minsk")]
        [InlineData(2, "London")]
        [InlineData(3, "Moscow")]
        public async void AddWeatherHistoryAsync_IfCityNamesInConfigCorrect_WriteDataInDatabase(int id, string name)
        {
            //Arrange
            var expected = new CityDTO { Id = id, CityName = name };
            var cts = new CancellationTokenSource();

            //Act
            var actualResult = await Record.ExceptionAsync(async () => await _weatherHistoryService.AddWeatherHistoryAsync(expected, cts.Token));

            //Assert
            Assert.Null(actualResult);
        }
    }
}
