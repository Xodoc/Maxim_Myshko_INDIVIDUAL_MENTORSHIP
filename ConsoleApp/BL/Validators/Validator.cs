using BL.Interfaces;
using BL.Validators.CustomExceptions;

namespace BL.Validators
{
    public class Validator : IValidator
    {
        public void ValidateCityName(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ValidatorException("\nInvalid data entered");
            }
        }

        public void ValidateNumberOfDays(int days)
        {
            if (days > 7 || days <= 0)
            {
                throw new ValidatorException("\nInvalid data entered");
            }
        }
    }
}
