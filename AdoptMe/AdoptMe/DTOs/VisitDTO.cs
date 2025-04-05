using AdoptMe.Data.Entities;

namespace AdoptMe.DTOs
{
    public class VisitDTO
    {
        public int PetId { get; set; }
        public virtual PetDTO? Pet { get; set; }
        public int VetId { get; set; }
        public virtual VetDTO? Vet { get; set; }
        public DateTime VisitDate { get; set; }
        public string Description { get; set; }
    }
}
