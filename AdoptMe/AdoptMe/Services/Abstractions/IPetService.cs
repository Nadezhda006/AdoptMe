using AdoptMe.DTOs;

namespace AdoptMe.Services.Abstractions
{
    public interface IPetService
    {
        Task<PetDTO> GetByIdAsync(int id);
        Task<ICollection<PetDTO>> GetAllAsync();
        Task CreateAsync(PetDTO petDto);
        Task UpdateAsync(PetDTO petDto);
        Task DeleteAsync(int petId);
        ICollection<PetDTO> GetByNameAndType(string name,string type);
        Task AddCatVisitAsync(VisitDTO VisitDTO);        
    }
}
