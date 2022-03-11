using AutoMapper;
using BL.DTOs;
using BL.Interfaces;
using DAL.Interfaces;
using Shared.Interfaces;
using System.Collections.Generic;
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
            _validator.ValidateConfigNames(_config.CityNames);

            var result = await _cityRepository.CheckAndCreateCities(_config.CityNames);
            
            return _mapper.Map<List<CityDTO>>(result);
        }
    }
}
