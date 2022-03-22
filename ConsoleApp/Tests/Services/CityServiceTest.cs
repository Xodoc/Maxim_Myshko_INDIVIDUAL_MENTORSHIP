using AutoMapper;
using BL.DTOs;
using BL.EqualityComparers;
using BL.Interfaces;
using BL.Mapping.Profiles;
using BL.Services;
using BL.Validators;
using DAL.Entities.WeatherHistoryEntities;
using DAL.Interfaces;
using Moq;
using Shared.Config;
using Shared.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace Tests.Services
{
    public class CityServiceTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICityRepository> _cityRepositoryMock;
        private readonly IValidator _validator;
        private readonly IConfiguration _config;
        private readonly ICityService _cityService;

        public CityServiceTest()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new CityProfile())).CreateMapper();
            _cityRepositoryMock = new Mock<ICityRepository>();
            _config = new ConfigurationTest();
            _validator = new Validator(_config);
            _cityService = new CityService(_mapper, _cityRepositoryMock.Object, _config, _validator);
        }

        [Fact]
        public async void CheckAndCreateCitiesAsync_IfCitiesDoesntExist_CreateCity()
        {
            //Arrange
            var expectedRepo = new List<City> 
            {
                new City { Id = 1, CityName = "Minsk"},
                new City { Id = 2, CityName = "London"},
                new City { Id = 3, CityName = "Moscow"},
                new City { Id = 4, CityName = "Warsaw"}
            };
            var expected = _mapper.Map<List<CityDTO>>(expectedRepo);
            var newCity = new CityDTO { Id = 5, CityName = "Paris" };

            expected.Add(newCity);

            _cityRepositoryMock.Setup(x => x.GetCitiesByCityNameAsync(It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(expectedRepo);

            _cityRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<City>())).ReturnsAsync(_mapper.Map<City>(newCity));

            //Act
            var actualResult = await _cityService.CheckAndCreateCitiesAsync();

            //Assert
            Assert.Equal(expected, actualResult, new CityDTOComparator());
        }
    }
}
