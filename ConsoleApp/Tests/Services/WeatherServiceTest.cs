using AutoFixture;
using AutoFixture.AutoMoq;
using BL.Interfaces;
using BL.Services;
using BL.Validators.CustomExceptions;
using DAL.Entities;
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
            
            expected.main.temp = temp;
            expected.weather[0].description = description;

            var expectedMessage = $"\nIn {expected.name} {expected.main.temp}°C now. {expected.weather[0].description}\n";

            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(It.IsAny<string>()))
                .ReturnsAsync(expected);

            //Act
            var actualResult = await _weatherService.GetWeatherAsync(expected.name);

            //Assert
            Assert.Equal(expectedMessage, actualResult);
        }

        [Theory]
        [InlineData("some data")]
        public async void GetWeatherAsync_WhenSendingIncorrectCityName_GettingWeatherMessageFailed(string cityName)
        {
            //Arrange                         
            var expected = _fixture.Create<Root>();

            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(It.IsAny<string>()))
                .ReturnsAsync(() => throw new ValidatorException("Incorrectly entered data"));

            //Act
            var actualResult = await Assert.ThrowsAsync<ValidatorException>(() => _weatherService.GetWeatherAsync(cityName));

            //Assert
            Assert.Equal("Incorrectly entered data", actualResult.Message);
        }
    }
}
