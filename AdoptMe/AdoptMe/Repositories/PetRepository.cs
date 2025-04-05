using AdoptMe.Data;
using AdoptMe.Data.Entities;
using AdoptMe.Repositories.Abstractions;

namespace AdoptMe.Repositories
{
    public class PetRepository : CrudRepository<Pet>, IPetRepository
    {
        private readonly ApplicationDbContext _context;

        public PetRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
