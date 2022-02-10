using BL.Interfaces;
using BL.Validators.CustomExceptions;
using Shared.Interfaces;

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
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ValidatorException("\nInvalid data entered");
            }
            else if (days > _config.MaxDays || days <= _config.MinDays)
            {
                throw new ValidatorException("\nInvalid data entered");
            }
        }
    }
}
