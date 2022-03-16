using BL.DTOs;
using System;
using System.Collections.Generic;

namespace BL.EqualityComparers
{
    public class WeatherHistoryDTOComparator : IEqualityComparer<WeatherHistoryDTO>
    {
        public bool Equals(WeatherHistoryDTO x, WeatherHistoryDTO y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;

            return x.CityId == y.CityId && x.Timestamp == y.Timestamp && x.Temp == y.Temp;
        }

        public int GetHashCode(WeatherHistoryDTO obj)
        {
            return HashCode.Combine(obj.CityId, obj.Timestamp, obj.Temp);
        }
    }
}
