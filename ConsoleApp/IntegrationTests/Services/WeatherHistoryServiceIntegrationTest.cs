using AutoMapper;
using BL.DTOs;
using BL.EqualityComparers;
using BL.Interfaces;
using DAL.Database;
using DAL.Entities.WeatherHistoryEntities;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace IntegrationTests.Services
{
    public class WeatherHistoryServiceIntegrationTest
    {
        private readonly IMapper _mapper;
        private readonly IWeatherHistoryService _weatherHistoryService;
        private readonly ApplicationDbContext _context;

        public WeatherHistoryServiceIntegrationTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddDI().AddLogging().BuildServiceProvider();

            _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _mapper = serviceProvider.GetService<IMapper>();
            _weatherHistoryService = serviceProvider.GetService<IWeatherHistoryService>();
        }

        [Theory]
        [InlineData("Minsk", "2022-03-04T00:00:00.0000000")]
        public async void GetWeatherHistoryAsync_WhenSendingCorrectData_GettingWeatherHistory(string cityName, string date)
        {
            //Arrange
            var expected = new WeatherHistoryDTO { CityId = 1, Timestamp = DateTime.Parse(date), Temp = -2.14 };
            var data = _mapper.Map<WeatherHistory>(expected);

            //Act
            using var transaction = _context.Database.BeginTransaction();

            await _context.WeatherHistories.AddAsync(data);
            await _context.SaveChangesAsync();

            var actualResult = await _weatherHistoryService.GetWeatherHistoriesAsync(cityName, date);

            //Assert
            Assert.Equal(expected, actualResult[0], new WeatherHistoryDTOComparator());

            await transaction.RollbackAsync();
        }
    }
}
