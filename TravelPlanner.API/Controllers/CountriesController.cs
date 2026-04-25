using Microsoft.AspNetCore.Mvc;
using TravelPlanner.Application.Interfaces;
using System.Threading.Tasks;

namespace TravelPlanner.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            return Ok(countries);
        }
    }
}
