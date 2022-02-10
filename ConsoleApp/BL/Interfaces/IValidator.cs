namespace BL.Interfaces
{
    public interface IValidator
    {
        void ValidateCityName(string value);

        void ValidateNumberOfDays(int value);
    }
}
