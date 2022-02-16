using BL.Interfaces;
using BL.Services;
using BL.Validators;
using BL.Validators.CustomExceptions;
using DAL.Interfaces;
using DAL.Repositories;
using Shared.Config;
using Shared.Interfaces;
using Xunit;

namespace IntegrationTests.Services
{
    public class WeatherServiceIntegrationTest
    {
        private readonly IConfiguration _configuration;
        private readonly IWeatherRepository _weatherRepository;
        private readonly IValidator _validator;
        private readonly IWeatherService _weatherService;

        public WeatherServiceIntegrationTest()
        {
            _configuration = new ConfigurationTest();
            _weatherRepository = new WeatherRepository(_configuration);
            _validator = new Validator(_configuration);
            _weatherService = new WeatherService(_weatherRepository, _validator);
        }

        [Theory]
        [InlineData("Yakutsk")]
        [InlineData("Morocco")]
        public async void GetWeatherAsync_WhenSendingCorrectCityName_GettingWeatherMessage(string cityName)
        {
            //Arrange
            var pattern = "([0-9])(.*?)\\.|(\\B\\W)(.*?)\\.";
            var descriptions = new string[]
            {
                "Dress warmly\\.",
                "It's fresh\\.",
                "Good weather\\.",
                "It's time to go to the beach\\."
            };
            var expectedRegexPattern = $"^In {cityName} {pattern} ({descriptions[0]}|{descriptions[1]}|{descriptions[2]}|{descriptions[3]})$";

            //Act
            var actualResult = await _weatherService.GetWeatherAsync(cityName);

            //Assert
            Assert.Matches(expectedRegexPattern, actualResult);
        }

        [Theory]
        [InlineData("some data")]
        [InlineData("")]
        [InlineData(" ")]
        public async void GetWeatherAsync_WhenSendingIncorrectCityName_GettingWeatherMessageFailed(string cityName)
        {
            //Arrange
            var expectedMessage = "\nInvalid data entered";

            //Act
            var actualResult = await Assert.ThrowsAsync<ValidatorException>(() => _weatherService.GetWeatherAsync(cityName));

            //Assert
            Assert.Equal(expectedMessage, actualResult.Message);
        }

        [Theory]
        [InlineData("Minsk", 3)]
        [InlineData("Morocco", 7)]
        public async void GetWeatherForecastAsync_WhenSendingCorrectData_GettingWeatherForecastMessage(string cityName, int days)
        {
            //Arrange
            var expectedMessage = "";
            var pattern = "([0-9])(.*?)\\.|(\\B\\W)(.*?)\\.";
            var descriptions = new string[]
            {
                "Dress warmly\\.",
                "It's fresh\\.",
                "Good weather\\.",
                "It's time to go to the beach\\."
            };

            for (int i = 0; i < days; i++)
            {
                expectedMessage += $"{cityName} weather forecast: {pattern} ({descriptions[0]}|{descriptions[1]}|{descriptions[2]}|{descriptions[3]})\n";
            }

            //Act
            var actualResult = await _weatherService.GetWeatherForecastAsync(cityName, days);

            //Assert
            Assert.Matches(expectedMessage, actualResult);
        }

        [Theory]
        [InlineData("Minsk", 8)]
        [InlineData("Some data", 2)]
        public async void GetWeatherForecastAsync_WhenSendingIncorrectData_GettingFailedMessage(string cityName, int days)
        {
            //Arrange
            var expectedMessage = "\nInvalid data entered";

            //Act
            var actualResult = await Assert.ThrowsAsync<ValidatorException>(() => _weatherService.GetWeatherForecastAsync(cityName, days));

            //Assert
            Assert.Equal(expectedMessage, actualResult.Message);
        }
    }
}
