using Microsoft.EntityFrameworkCore;
using TravelPlanner.Application.DTOs;
using TravelPlanner.Application.Interfaces;
using TravelPlanner.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPlanner.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly AppDbContext _context;

        public CountryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CountryDto>> GetAllCountriesAsync()
        {
            return await _context.Countries
                .AsNoTracking()
                .Select(c => new CountryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
