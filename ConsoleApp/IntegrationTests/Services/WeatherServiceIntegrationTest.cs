using BL.Interfaces;
using BL.Services;
using BL.Validators;
using BL.Validators.CustomExceptions;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using IntegrationTests.Config;
using Shared.Interfaces;
using Xunit;

namespace IntegrationTests.Services
{
    public class WeatherServiceIntegrationTest
    {
        private readonly IConfiguration _configuration;
        private readonly IWeatherRepository _weatherRepository;
        private readonly IValidator<Root> _validator;
        private readonly IWeatherService _weatherService;

        public WeatherServiceIntegrationTest()
        {
            _configuration = new ConfigurationTest();
            _weatherRepository = new WeatherRepository(_configuration);
            _validator = new Validator<Root>();
            _weatherService = new WeatherService(_weatherRepository, _validator);
        }

        [Theory]
        [InlineData("Yakutsk")]
        [InlineData("Morocco")]
        public async void GetWeatherAsync_WhenSendingCorrectCityName_GettingWeatherMessage(string cityName)
        {
            //Arrange
            string[] des = new string[]
            {
                "Dress warmly\\.", "It's fresh\\.",
                "Good weather\\.", "It's time to go to the beach\\."
            };

            //Act
            var result = await _weatherService.GetWeatherAsync(cityName);

            //Assert
            Assert.Matches($"^In {cityName} ([0-9])(.*?)\\.|(\\B\\W)(.*?)\\. ({des[0]}|{des[1]}|{des[2]}|{des[3]})$", result);
        }

        [Theory]
        [InlineData("some data")]
        [InlineData("")]
        [InlineData(" ")]
        public async void GetWeatherAsync_WhenSendingIncorrectCityName_GettingWeatherMessageFailed(string cityName)
        {
            //Arrange
            var expectedMessage = "\nIncorrectly entered data";

            //Act
            var actualResult = await Assert.ThrowsAsync<ValidatorException>(() => _weatherService.GetWeatherAsync(cityName));

            //Assert
            Assert.Equal(expectedMessage, actualResult.Message);
        }
    }
}
