namespace BL.Interfaces
{
    public interface IValidator<T> where T : class
    {
        void Validate(T values);

        void ValidateCityName(string values);
    }
}
