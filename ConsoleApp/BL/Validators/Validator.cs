using BL.Interfaces;
using BL.Validators.CustomExceptions;

namespace BL.Validators
{
    public class Validator<T> : IValidator<T> where T : class
    {
        public void Validate(T value)
        {
            if (value == null)
            {
                throw new ValidatorException("\nIncorrectly entered data");
            }
        }

        public void ValidateCityName(string cityName) 
        {
            if (string.IsNullOrWhiteSpace(cityName)) 
            {
                throw new ValidatorException("\nIncorrectly entered data");
            }
        }
    }
}
