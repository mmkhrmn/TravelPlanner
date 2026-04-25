using System.Collections.Generic;

namespace TravelPlanner.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int CountryId { get; set; }
        public virtual Country? Country { get; set; }

        public virtual ICollection<PopularPlace> PopularPlaces { get; set; } = new List<PopularPlace>();
    }
}
