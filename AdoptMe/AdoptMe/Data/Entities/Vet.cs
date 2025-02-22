namespace AdoptMe.Data.Entities
{
    public class Vet:BaseEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phonenumber { get; set; }
        public virtual ICollection<Visit>? Visits{ get; set; }

    }
}
