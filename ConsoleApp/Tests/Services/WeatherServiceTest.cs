using AutoFixture;
using AutoFixture.AutoMoq;
using BL.Interfaces;
using BL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Moq;
using Tests.Fixtures;
using Xunit;

namespace Tests.Services
{
    public class WeatherServiceTest
    {
        private readonly Mock<IWeatherRepository> _weatherRepositoryMock;
        private readonly IWeatherService _weatherService;
        private readonly Fixture _fixture;
        private WeatherFixture _weatherFixture;

        public WeatherServiceTest()
        {
            _weatherFixture = new WeatherFixture();
            _fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());
            _weatherRepositoryMock = _fixture.Freeze<Mock<IWeatherRepository>>();
            _weatherService = _fixture.Create<WeatherService>();   
        }

        [Fact]
        public async void GetWeatherAsync_WhenSendingCorrectCityName_GettingWeatherMessage()
        {
            //Arrange                         
            var expected = _fixture.Create<Root>();
            
            var weatherFixture = _weatherFixture.GetWeatherDescription();
            expected.main.temp = weatherFixture[0].Temp;
            expected.weather[0].description = weatherFixture[0].Description;

            var expectedMessage = $"In {expected.name}{expected.main.temp}°C now. {expected.weather[0].description}\n";

            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(It.IsAny<string>()))
                .ReturnsAsync(expected);

            //Act
            var actualResult = await _weatherService.GetWeatherAsync(expected.name);

            //Assert
            Assert.Equal(expectedMessage, actualResult);
        }

        [Fact]
        public async void GetWeatherAsync_WhenSendingCorrectCityName_GettingWeatherMessage1()
        {
            //Arrange                         
            var expected = _fixture.Create<Root>();
            var weatherFixtures = _weatherFixture.GetWeatherDescription();
            expected.main.temp = weatherFixtures[1].Temp;
            expected.weather[0].description = weatherFixtures[1].Description;

            var expectedMessage = $"In {expected.name}{expected.main.temp}°C now. {expected.weather[0].description}\n";

            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(It.IsAny<string>()))
                .ReturnsAsync(expected);

            //Act
            var actualResult = await _weatherService.GetWeatherAsync(expected.name);

            //Assert
            Assert.Equal(expectedMessage, actualResult);
        }

        [Fact]
        public async void GetWeatherAsync_WhenSendingCorrectCityName_GettingWeatherMessage2()
        {
            //Arrange                         
            var expected = _fixture.Create<Root>();
            var weatherFixtures = _weatherFixture.GetWeatherDescription();
            expected.main.temp = weatherFixtures[2].Temp;
            expected.weather[0].description = weatherFixtures[2].Description;

            var expectedMessage = $"In {expected.name}{expected.main.temp}°C now. {expected.weather[0].description}\n";

            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(It.IsAny<string>()))
                .ReturnsAsync(expected);

            //Act
            var actualResult = await _weatherService.GetWeatherAsync(expected.name);

            //Assert
            Assert.Equal(expectedMessage, actualResult);
        }

        [Fact]
        public async void GetWeatherAsync_WhenSendingCorrectCityName_GettingWeatherMessage3()
        {
            //Arrange                         
            var expected = _fixture.Create<Root>();
            var weatherFixtures = _weatherFixture.GetWeatherDescription();
            expected.main.temp = weatherFixtures[3].Temp;
            expected.weather[0].description = weatherFixtures[3].Description;

            var expectedMessage = $"In {expected.name}{expected.main.temp}°C now. {expected.weather[0].description}\n";

            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(It.IsAny<string>()))
                .ReturnsAsync(expected);

            //Act
            var actualResult = await _weatherService.GetWeatherAsync(expected.name);

            //Assert
            Assert.Equal(expectedMessage, actualResult);
        }
    }
}
