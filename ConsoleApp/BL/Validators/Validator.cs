using BL.Interfaces;
using BL.Validators.CustomExceptions;
using DAL.Entities.WeatherHistoryEntities;
using Shared.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BL.Validators
{
    public class Validator : IValidator
    {
        private readonly IConfiguration _config;

        public Validator(IConfiguration config)
        {
            _config = config;
        }

        public void ValidateCityName(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ValidatorException("\nInvalid data entered");
            }
        }

        public void ValidateModel(string cityName, int days)
        {
            ValidateCityName(cityName);

            if (days > _config.MaxDays || days <= _config.MinDays)
            {
                throw new ValidatorException("\nInvalid data entered");
            }
        }

        public void ValidateCityNames(IEnumerable<string> cityNames)
        {
            if (cityNames.Count() == 0 || cityNames == null)
            {
                throw new ValidatorException("\nInvalid data entered");
            }
        }

        public void ValidateWeatherHistories(WeatherHistory history)
        {
            if (history == null)
            {
                throw new ValidatorException("Invalid data in appsettings.json");
            }
        }

        public void ValidateConfigNames(IEnumerable<string> names) 
        {
            if (names.Contains(string.Empty)) 
            {
                throw new ValidatorException("Some names in config are empty");
            }
        }
    }
}
