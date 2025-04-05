using AutoMapper;
using AdoptMe.Data.Entities;
using AdoptMe.DTOs;

namespace AdoptMe.Profiles
{
    public class PetProfile : Profile
    {
        public PetProfile()
        {
            CreateMap<Pet, PetDTO>()
                .ReverseMap();
        }
    }
}
