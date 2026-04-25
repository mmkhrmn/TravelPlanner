namespace TravelPlanner.Domain.Entities
{
    public class PopularPlace
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int CityId { get; set; }
        public virtual City? City { get; set; }
    }
}
