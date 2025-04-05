using AdoptMe.Data.Entities;

namespace AdoptMe.Repositories.Abstractions
{
    public interface IVisitRepository
    {
        Task CreateAsync(Visit visit);
        Task DeleteByIdAsync(int id);
        Task<ICollection<Visit>> GetAllAsync();
        ICollection<Visit> GetByFilter(Func<Visit, bool> predicate);
        Task<Visit?> GetByIdAsync(int id);
    }
}
