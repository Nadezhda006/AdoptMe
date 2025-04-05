using AdoptMe.Data.Entities;
using static AdoptMe.Repositories.Abstractions.ICrudRepository;

namespace AdoptMe.Repositories.Abstractions
{
    public interface IPetRepository : ICrudRepository<Pet>
    {

    }
}
