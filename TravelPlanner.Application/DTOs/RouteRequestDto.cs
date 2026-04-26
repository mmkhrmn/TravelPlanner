namespace TravelPlanner.Application.DTOs
{
    public class RouteRequestDto
    {
        public string CountryName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string WakeUpTime { get; set; } = string.Empty;
        public string UserNotes { get; set; } = string.Empty;
    }
}
