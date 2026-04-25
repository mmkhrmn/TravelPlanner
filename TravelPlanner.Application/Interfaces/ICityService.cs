using TravelPlanner.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelPlanner.Application.Interfaces
{
    public interface ICityService
    {
        Task<List<CityDto>> GetCitiesByCountryAsync(int countryId);
    }
}
