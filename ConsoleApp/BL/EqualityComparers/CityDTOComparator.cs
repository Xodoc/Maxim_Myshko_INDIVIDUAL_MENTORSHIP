using BL.DTOs;
using System;
using System.Collections.Generic;

namespace BL.EqualityComparers
{
    public class CityDTOComparator : IEqualityComparer<CityDTO>
    {
        public bool Equals(CityDTO x, CityDTO y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;

            return x.Id == y.Id && x.CityName == y.CityName;
        }

        public int GetHashCode(CityDTO obj)
        {
            return HashCode.Combine(obj.Id, obj.CityName);
        }
    }
}
