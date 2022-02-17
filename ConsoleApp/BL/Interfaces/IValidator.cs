using System.Collections.Generic;

namespace BL.Interfaces
{
    public interface IValidator
    {
        void ValidateCityName(string value);

        void ValidateModel(string cityName, int days);

        void ValidateCityNames(List<string> cityNames);
    }
}
