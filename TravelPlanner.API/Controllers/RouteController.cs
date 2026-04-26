using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TravelPlanner.Application.DTOs;
using TravelPlanner.Application.Interfaces;

namespace TravelPlanner.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateRoute([FromBody] RouteRequestDto request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest("Eksik veya hatalı veri gönderildi.");
            }

            try
            {
                // DİKKAT: Hatanın çözüldüğü satır burası! 
                // Artık request içindeki WakeUpTime ve UserNotes verilerini de servise gönderiyoruz.
                var result = await _routeService.GenerateRouteAsync(
                    request.CityName, 
                    request.CountryName, 
                    request.WakeUpTime, 
                    request.UserNotes
                );
                
                return Ok(new { Message = "Başarılı", DraftRoute = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Sunucu Hatası", Details = ex.Message });
            }
        }
    }
}