using System;

namespace BL.Validators.CustomExceptions
{
    public class ValidatorException : Exception
    {
        public ValidatorException(string message) : base(message)
        {
        }
    }
}
