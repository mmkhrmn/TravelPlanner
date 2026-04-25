using Microsoft.AspNetCore.Mvc;
using TravelPlanner.Application.Interfaces;
using System.Threading.Tasks;

namespace TravelPlanner.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByCountry([FromQuery] int countryId)
        {
            if (countryId <= 0) return BadRequest("countryId is required");

            var cities = await _cityService.GetCitiesByCountryAsync(countryId);
            return Ok(cities);
        }
    }
}
