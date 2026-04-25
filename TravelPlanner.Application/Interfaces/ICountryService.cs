using TravelPlanner.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelPlanner.Application.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryDto>> GetAllCountriesAsync();
    }
}
