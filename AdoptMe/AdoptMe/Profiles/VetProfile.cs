using AutoMapper;
using AdoptMe.Data.Entities;
using AdoptMe.DTOs;

namespace AdoptMe.Profiles
{
    public class VetProfile : Profile
    {
        public VetProfile()
        {
            CreateMap<Vet, VetDTO>()
                .ReverseMap();
        }
    }
}