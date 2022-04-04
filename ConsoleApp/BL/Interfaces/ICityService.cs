using BL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ICityService
    {
        Task<List<CityDTO>> CheckAndCreateCitiesAsync();
        
        Task<List<CityDTO>> GetCitiesByCityNamesAsync(IEnumerable<string> cityNames);
    }
}
