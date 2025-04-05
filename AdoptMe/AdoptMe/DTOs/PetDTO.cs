using AdoptMe.Data.Entities;

namespace AdoptMe.DTOs
{
    public class PetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Color { get; set; }
        public double Weight { get; set; }
        public string ImageURL { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public virtual ICollection<VisitDTO>? Visits { get; set; }
    }
}
