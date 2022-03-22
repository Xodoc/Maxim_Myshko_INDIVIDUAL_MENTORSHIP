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

        [Fact]
        public async void GetWeatherHistoryAsync_WhenSendingCorrectData_GettingWeatherHistory()
        {
            //Arrange
            var cityName = "Minsk";
            var from = "12.03.2022";
            var to = "16.03.2022";
            var fixture = new List<WeatherHistory>
            {
                new WeatherHistory { CityId = 9, Timestamp = DateTime.Parse(from), Temp = -2.14 },
                new WeatherHistory { CityId = 9, Timestamp = DateTime.Parse("13.03.2022"), Temp = -2.14 },
                new WeatherHistory { CityId = 9, Timestamp = DateTime.Parse("11.03.2022"), Temp = 23 },
                new WeatherHistory { CityId = 9, Timestamp = DateTime.Parse(from), Temp = -15.1 }
            };

            var data = fixture.FindAll(x => x.Timestamp >= DateTime.Parse(from) && x.Timestamp <= DateTime.Parse(to));

            await _context.WeatherHistories.AddRangeAsync(fixture);
            await _context.SaveChangesAsync();

            var expected = _mapper.Map<List<WeatherHistoryDTO>>(data);

            //Act
            var actualResult = await _weatherHistoryService.GetWeatherHistoriesAsync(cityName, DateTime.Parse(from), DateTime.Parse(to));

            //Assert
            Assert.Equal(expected, actualResult, new WeatherHistoryDTOComparator());
        }

        [Fact]
        public async void GetWeatherHistoryAsync_WhenSendingIncorrectData_GettingWeatherHistory()
        {
            //Arrange
            var cityName = "Minsk";
            var from = "12.03.2022";
            var to = "16.03.2022";
            var expectedList = new List<WeatherHistoryDTO>();
            var someData = new WeatherHistory { CityId = 9, Timestamp = DateTime.Parse("11.03.2022"), Temp = -2.14 };

            //Act
            await _context.WeatherHistories.AddAsync(someData);
            await _context.SaveChangesAsync();

            var actualResult = await _weatherHistoryService.GetWeatherHistoriesAsync(cityName, DateTime.Parse(from), DateTime.Parse(to));

            //Asssert
            Assert.Equal(expectedList, actualResult, new WeatherHistoryDTOComparator());
        }
    }
}
