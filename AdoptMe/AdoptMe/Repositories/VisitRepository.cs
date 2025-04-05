using AdoptMe.Data;
using AdoptMe.Data.Entities;
using AdoptMe.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AdoptMe.Repositories
{
    public class VisitRepository : IVisitRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Visit> _set;

        public VisitRepository(ApplicationDbContext context)
        {
            _context = context;
            _set = _context.Set<Visit>();
        }

        public async Task CreateAsync(Visit visit)
        {
            _set.Add(visit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            Visit visit = await GetByIdAsync(id);
            if (visit != null)
            {
                _set.Remove(visit);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Item with id {id} does not exist.");
            }
        }

        public async Task<ICollection<Visit>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public ICollection<Visit> GetByFilter(Func<Visit, bool> predicate)
        {
            return _set
                .Where(predicate)
                .ToList();
        }

        public async Task<Visit?> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<Visit> UpdateAsync(Visit visit)
        {
            _set.Update(visit);
            await _context.SaveChangesAsync();
            return visit;
        }
    }
}
