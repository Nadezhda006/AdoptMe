using AdoptMe.Data.Entities;
using AdoptMe.DTOs;
using AdoptMe.Repositories.Abstractions;
using AdoptMe.Services.Abstractions;
using AutoMapper;

namespace AdoptMe.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petsRepository;
        private readonly IVisitRepository _visitRepository;
        private readonly IMapper _mapper;
        public PetService(IPetRepository catsRepository, IVisitRepository visitRepository, IMapper mapper)
        {
            _petsRepository = catsRepository;
            _visitRepository = visitRepository;
            _mapper = mapper;
        }
        public async Task AddCatVisitAsync(VisitDTO VisitDTO)
        {
            var visit = _mapper.Map<Visit>(VisitDTO);
            await _visitRepository.CreateAsync(visit);
        }

        public async Task CreateAsync(PetDTO petDto)
        {
            var pet = _mapper.Map<Pet>(petDto);
            await _petsRepository.CreateAsync(pet);
        }

        public async Task DeleteAsync(int catId)
        {
            await _petsRepository.DeleteByIdAsync(catId);
        }

        public async Task<ICollection<PetDTO>> GetAllAsync()
        {
            var pets = await _petsRepository.GetAllAsync();
            return _mapper.Map<ICollection<PetDTO>>(pets);
        }

        public async Task<PetDTO> GetByIdAsync(int id)
        {
            var pet = await _petsRepository.GetByIdAsync(id);
            return _mapper.Map<PetDTO>(pet);
        }

        public ICollection<PetDTO> GetByName(string name)
        {
            var pets = _petsRepository.GetByFilter(pet => pet.Name == name);
            return _mapper.Map<ICollection<PetDTO>>(pets);
        }

        public async Task UpdateAsync(PetDTO petDto)
        {
            var pet = _mapper.Map<Pet>(petDto);
            await _petsRepository.UpdateAsync(pet);
        }
    }
}
