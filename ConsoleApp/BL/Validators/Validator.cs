using BL.Interfaces;
using BL.Validators.CustomExceptions;

namespace BL.Validators
{
    public class Validator<T> : IValidator<T> where T : class
    {
        public void ValidateCityName(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ValidatorException("\nIncorrectly entered data");
            }
        }
    }
}
