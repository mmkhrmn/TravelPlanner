using Microsoft.EntityFrameworkCore;
using TravelPlanner.Application.DTOs;
using TravelPlanner.Application.Interfaces;
using TravelPlanner.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPlanner.Application.Services
{
    public class CityService : ICityService
    {
        private readonly AppDbContext _context;

        public CityService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CityDto>> GetCitiesByCountryAsync(int countryId)
        {
            return await _context.Cities
                .AsNoTracking()
                .Where(c => c.CountryId == countryId)
                .Select(c => new CityDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    CountryId = c.CountryId
                })
                .ToListAsync();
        }
    }
}
