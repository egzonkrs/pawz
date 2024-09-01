using AutoMapper;
using Pawz.Application.Models;
using Pawz.Domain.Entities;
using Pawz.Application.Models.Pet;
using System.Linq;

namespace Pawz.Application.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PetCreateRequest, Pet>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<UserPetRequest,Pet>().ReverseMap();
        CreateMap<Pet,UserPetResponse>().ReverseMap();
        CreateMap<UserPetRequest, UserPetResponse>().ReverseMap();
    }
}
