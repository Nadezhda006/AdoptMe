using AutoMapper;
using AdoptMe.Data.Entities;
using AdoptMe.DTOs;

namespace AdoptMe.Profiles
{
    public class VisitProfile : Profile
    {
        public VisitProfile()
        {
            CreateMap<Visit, VisitDTO>()
                .ReverseMap();
        }
    }
}