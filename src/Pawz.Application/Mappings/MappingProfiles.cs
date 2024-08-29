using AutoMapper;
using Pawz.Domain.Entities;
using Pawz.Application.Models.Pet;
using System.Linq;

namespace Pawz.Application.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UserPetRequest,Pet>().ReverseMap();
        CreateMap<Pet,UserPetResponse>().ReverseMap();
        CreateMap<UserPetRequest, UserPetResponse>().ReverseMap();
    }
}
