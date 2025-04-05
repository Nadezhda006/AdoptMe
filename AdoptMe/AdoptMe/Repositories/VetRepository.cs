using AdoptMe.Data.Entities;
using AdoptMe.Data;
using AdoptMe.Repositories.Abstractions;

namespace AdoptMe.Repositories
{
    public class VetRepository : CrudRepository<Vet>, IVetRepository
    {
        public VetRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
