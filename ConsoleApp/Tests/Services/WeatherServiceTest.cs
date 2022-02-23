using AutoFixture;
using AutoFixture.AutoMoq;
using BL.Interfaces;
using BL.Services;
using BL.Validators.CustomExceptions;
using DAL.Entities;
using DAL.Entities.WeatherForecastEntities;
using DAL.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace Tests.Services
{
    public class WeatherServiceTest
    {
        private readonly Mock<IWeatherRepository> _weatherRepositoryMock;
        private readonly IWeatherService _weatherService;
        private readonly Fixture _fixture;

        public WeatherServiceTest()
        {
            _fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());
            _weatherRepositoryMock = _fixture.Freeze<Mock<IWeatherRepository>>();
            _weatherService = _fixture.Create<WeatherService>();
        }

        [Theory]
        [InlineData(-12, "Dress warmly.")]
        [InlineData(0, "It's fresh.")]
        [InlineData(25, "Good weather.")]
        [InlineData(40, "It's time to go to the beach.")]
        public async void GetWeatherAsync_WhenSendingCorrectCityName_GettingWeatherMessage(double temp, string description)
        {
            //Arrange                         
            var expected = _fixture.Create<CurrentWeather>();
            var cts = new CancellationTokenSource();
            expected.Main.Temp = temp;
            expected.Weather[0].Description = description;

            var expectedMessage = $"\nIn {expected.Name} {expected.Main.Temp}°C now. {expected.Weather[0].Description}\n";

            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(It.IsAny<string>(), It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(expected);

            //Act
            var actualResult = await _weatherService.GetWeatherAsync(expected.Name);

            //Assert
            Assert.Equal(expectedMessage, actualResult);
        }

        [Theory]
        [InlineData("some data")]
        public async void GetWeatherAsync_WhenSendingIncorrectCityName_GettingWeatherMessageFailed(string cityName)
        {
            //Arrange                         
            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(It.IsAny<string>(), It.IsAny<CancellationTokenSource>()))
                .ReturnsAsync(() => throw new ValidatorException("Incorrectly entered data"));

            //Act
            var actualResult = await Assert.ThrowsAsync<ValidatorException>(() => _weatherService.GetWeatherAsync(cityName));

            //Assert
            Assert.Equal("Incorrectly entered data", actualResult.Message);
        }

        [Theory]
        [InlineData(-12, "Dress warmly.")]
        [InlineData(0, "It's fresh.")]
        [InlineData(25, "Good weather.")]
        public async void GetWetherForecastAsync_WhenSendingCorrectData_GettingWeatherForecastMessage(double temp, string description)
        {
            //Arrange
            var weatherForecast = _fixture.Create<WeatherForecast>();
            var expectedMessage = "";
            var numberOfDays = 1;
            foreach (var day in weatherForecast.Daily)
            {
                day.Main.Temp = temp;
                day.Weather[0].Description = description;
                expectedMessage += $"{weatherForecast.CityName} weather forecast:\nDay {numberOfDays++}: {day.Main.Temp}°C. {day.Weather[0].Description}\n";
            }

            _weatherRepositoryMock.Setup(x => x.GetWeatherForecastAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(weatherForecast);

            //Act
            var actualResult = await _weatherService.GetWeatherForecastAsync(weatherForecast.CityName, weatherForecast.Daily.Count);

            //Assert
            Assert.Equal(expectedMessage, actualResult);
        }

        [Theory]
        [InlineData("some data", 23)]
        public async void GetWeatherForecastAsync_WhenSendingIncorrectData_GettingMessageFailed(string cityName, int days)
        {
            //Arrange
            var expectedMessage = "Invalid data entered";

            _weatherRepositoryMock.Setup(x => x.GetWeatherForecastAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ThrowsAsync(new ValidatorException("Invalid data entered"));

            //Act
            var actualResult = await Assert.ThrowsAsync<ValidatorException>(() => _weatherService.GetWeatherForecastAsync(cityName, days));

            //Asseret
            Assert.Equal(expectedMessage, actualResult.Message);
        }

        [Fact]
        public async void GetMaxTemperatureAsync_WhenSendingCorrectData_GettingSuccessMessage()
        {
            //Arrange
            var expectedData = new List<TemperatureInfo>
            {
                new TemperatureInfo { CityName = "Minsk", Temp = 12, RunTime = 13, FailedRequest = 0, SuccessfullRequest = 1 },
                new TemperatureInfo { CityName = "Brest", Temp = 23, RunTime = 1, FailedRequest = 0, SuccessfullRequest = 1 },
                new TemperatureInfo { CityName = "dwadwd", Temp = 0, RunTime = 14, FailedRequest = 1, SuccessfullRequest = 0 }
            };
            var expectedCopy = new List<TemperatureInfo>
            {
                new TemperatureInfo { CityName = "Minsk", Temp = 12, RunTime = 13, FailedRequest = 0, SuccessfullRequest = 1 },
                new TemperatureInfo { CityName = "Brest", Temp = 23, RunTime = 1, FailedRequest = 0, SuccessfullRequest = 1 },
                new TemperatureInfo { CityName = "dwadwd", Temp = 0, RunTime = 14, FailedRequest = 1, SuccessfullRequest = 0 }
            };

            var cityNames = expectedData.Select(x => x.CityName).ToList();
            var maxtemp = expectedData.Max(t => t.Temp);
            var info = expectedData.FirstOrDefault(x => x.Temp == maxtemp);
            info.SuccessfullRequest = expectedData.Select(x => x.SuccessfullRequest).Sum();
            info.FailedRequest = expectedData.Select(x => x.FailedRequest).Sum();

            var expectedMessage = $"City with the highest temperature {info.Temp}°C: {info.CityName}." +
                  $"\r\nSuccessful request count: {info.SuccessfullRequest}, failed: {info.FailedRequest}, canceled: {info.Canceled}.\r\n";

            _weatherRepositoryMock.Setup(x => x.GetTemperaturesAsync(It.IsAny<IEnumerable<string>>())).ReturnsAsync(expectedCopy);

            //Act
            var actualResult = await _weatherService.GetMaxTemperatureAsync(cityNames);

            //Assert
            Assert.Equal(expectedMessage, actualResult);
        }

        [Fact]
        public async void GetMaxTemperatureAsync_WhenSendingIncorrectData_GettingFailedMessage()
        {
            //Arrange
            var input = new List<string>();
            var expectedMessage = "Invalid data entered";

            _weatherRepositoryMock.Setup(x => x.GetTemperaturesAsync(It.IsAny<List<string>>()))
                .Throws(new ValidatorException("Invalid data entered"));

            //Act
            var actualResult = await Assert.ThrowsAsync<ValidatorException>(() => _weatherService.GetMaxTemperatureAsync(input));

            //Asseret
            Assert.Equal(expectedMessage, actualResult.Message);
        }
    }
}
