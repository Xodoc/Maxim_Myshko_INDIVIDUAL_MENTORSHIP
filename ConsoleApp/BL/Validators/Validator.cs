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
            ValidateName(cityName, "\nInvalid data entered");
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
            foreach (var name in names)
            {
                ValidateName(name, "Some names in config are empty");
            }
        }

        private void ValidateName(string name, string message)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ValidatorException(message);
            }
        }
    }
}
