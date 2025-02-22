namespace AdoptMe.Data.Entities
{
    public class Visit
    {
        public int PetId { get; set; }
        public virtual Pet? Pet { get; set; }
        public int VetId { get; set; }
        public virtual Vet? Vet { get; set; }
        public DateTime VisitDate { get; set; }
        public string Description { get; set; }

    }
}
