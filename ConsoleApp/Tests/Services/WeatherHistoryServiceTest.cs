using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using BL.DTOs;
using BL.EqualityComparers;
using BL.Interfaces;
using BL.Mapping.Profiles;
using BL.Services;
using DAL.Entities.WeatherHistoryEntities;
using DAL.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.Services
{
    public class WeatherHistoryServiceTest
    {
        private readonly IMapper _mapper;
        private readonly IWeatherRepository _weatherRepository;
        private readonly IWeatherHistoryService _weatherHistoryService;
        private readonly Mock<IWeatherHistoryRepository> _weatherHistoryRepositoryMock;
        private readonly Fixture _fixture;


        public WeatherHistoryServiceTest()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new WeatherHistoryProfile())).CreateMapper();
            _fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());
            _weatherHistoryRepositoryMock = _fixture.Freeze<Mock<IWeatherHistoryRepository>>();
            _weatherRepository = _fixture.Freeze<IWeatherRepository>();
            _weatherHistoryService = new WeatherHistoryService(_weatherHistoryRepositoryMock.Object, _weatherRepository, _mapper);
        }

        [Theory]
        [InlineData("Minsk", "16.03.2022", "17.03.2022")]
        public async void GetWeatherHistoryAsync_WhenSendingCorrectData_GettingWeatherHistory(string cityName, string from, string to)
        {
            //Arrange
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var expectedFromRepo = _fixture.Create<List<WeatherHistory>>();
            var expected = _mapper.Map<List<WeatherHistoryDTO>>(expectedFromRepo);

            _weatherHistoryRepositoryMock.Setup(x =>
            x.GetWeatherHistoriesAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(expectedFromRepo);

            //Act
            var actualResult = await _weatherHistoryService.GetWeatherHistoriesAsync(cityName, DateTime.Parse(from), DateTime.Parse(to));

            //Assert
            Assert.Equal(expected, actualResult, new WeatherHistoryDTOComparator());
        }
    }
}
