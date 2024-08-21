using AutoMapper;
using Pawz.Application.Models;
using Pawz.Domain.Entities;

namespace Pawz.Application.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PetCreateRequest, Pet>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
