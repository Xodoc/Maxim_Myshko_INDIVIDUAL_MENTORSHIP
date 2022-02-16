namespace Shared.Interfaces
{
    public interface IConfiguration
    {
        string APIKey { get; }

        string URL { get; }

        string Units { get; }

        string Lang { get; }

        string URLGeo { get; }

        string Forecast { get; }

        int MaxDays { get; }

        int MinDays { get; }

        int Hours { get; }
    }
}
