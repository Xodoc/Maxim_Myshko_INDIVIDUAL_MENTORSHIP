﻿namespace Shared.Interfaces
{
    public interface IConfiguration
    {
        string APIKey { get; }

        string URL { get; }

        string Units { get; }

        string Lang { get; }

        string URLGeo { get; }

        string URLOneCall { get; }

        string Exclude { get; }
    }
}
