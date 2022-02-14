using AutoFixture;
using AutoFixture.AutoMoq;
using BL.Interfaces;
using BL.Services;
using BL.Validators.CustomExceptions;
using DAL.Entities;
using DAL.Entities.WeatherForecastEntities;
using DAL.Interfaces;
using Moq;
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
            var expected = _fixture.Create<Root>();

            expected.Main.Temp = temp;
            expected.Weather[0].Description = description;

            var expectedMessage = $"\nIn {expected.Name} {expected.Main.Temp}°C now. {expected.Weather[0].Description}\n";

            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(It.IsAny<string>()))
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
            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(It.IsAny<string>()))
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

            foreach (var day in weatherForecast.Daily)
            {
                day.Main.Temp = temp;
                day.Weather[0].Description = description;
                expectedMessage += $"{weatherForecast.CityName} weather forecast:\n{day.Date.DayOfWeek}: {day.Main.Temp}°C. {day.Weather[0].Description}\n";
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
    }
}
