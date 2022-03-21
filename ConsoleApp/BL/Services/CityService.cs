using AutoMapper;
using BL.DTOs;
using BL.Interfaces;
using DAL.Entities.WeatherHistoryEntities;
using DAL.Interfaces;
using Serilog;
using Shared.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services
{
    public class CityService : ICityService
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;
        private readonly IConfiguration _config;
        private readonly IValidator _validator;
        public CityService(IMapper mapper, ICityRepository cityRepository, IConfiguration config, IValidator validator)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _config = config;
            _validator = validator;
        }

        public async Task<List<CityDTO>> CheckAndCreateCitiesAsync()
        {
            Log.Information("Method CheckAndCreateCitiesAsync has been run!");

            _validator.ValidateConfigNames(_config.CityNames);

            var oldCities = await _cityRepository.GetCitiesByCityNameAsync(_config.CityNames);

            var newCities = new List<City>();

            foreach (var name in _config.CityNames)
            {
                var city = oldCities.FirstOrDefault(x => x.CityName == name);

                if (city == null)
                {
                    newCities.Add(await _cityRepository.CreateAsync(new City { CityName = name }));
                }
                else
                {                   
                    newCities.Add(city);
                }               
            }

            Log.Information("Method CheckAndCreateCitiesAsync is complited!");

            return _mapper.Map<List<CityDTO>>(newCities);
        }
    }
}
