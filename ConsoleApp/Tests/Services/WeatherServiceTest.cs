using AutoFixture;
using AutoFixture.AutoMoq;
using BL.DTOs;
using BL.Interfaces;
using Moq;
using Xunit;

namespace Tests.Services
{
    public class WeatherServiceTest
    {
        private readonly Mock<IWeatherService> _weatherServiceMock;
        private readonly IWeatherService _weatherService;
        private readonly Fixture _fixture;

        public WeatherServiceTest()
        {
            _fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());
            _weatherServiceMock = _fixture.Freeze<Mock<IWeatherService>>();
            _weatherService = _fixture.Create<IWeatherService>();
        }

        [Fact]
        public async void GetWeatherAsync_WhenSendingCorrectCityName_GettingWeather() 
        {
            //Arrange                        
            var cityName = _fixture.Create<string>(); 
            var expected = _fixture.Create<WeatherDTO>();            
            
            _weatherServiceMock.Setup(x => x.GetWeatherAsync(It.IsAny<string>()))
                .ReturnsAsync(expected);

            //Act
            var actualWeatherDTO = await _weatherService.GetWeatherAsync(cityName);
            
            //Assert
            Assert.Equal(expected, actualWeatherDTO);
        }
    }
}
