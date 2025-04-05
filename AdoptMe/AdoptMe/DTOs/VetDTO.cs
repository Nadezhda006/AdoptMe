using AdoptMe.Data.Entities;

namespace AdoptMe.DTOs
{
    public class VetDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phonenumber { get; set; }
        public virtual ICollection<VisitDTO>? Visits { get; set; }
    }
}
