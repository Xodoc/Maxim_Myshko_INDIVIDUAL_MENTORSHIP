namespace BL.Interfaces
{
    public interface IValidator<T> where T : class
    {
        void ValidateCityName(string values);
    }
}
