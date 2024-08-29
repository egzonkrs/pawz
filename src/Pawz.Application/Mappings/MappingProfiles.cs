using AutoMapper;
using Pawz.Domain.Entities;
using Pawz.Application.Models.Pet;
using System.Linq;

namespace Pawz.Application.Mappings;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PetRequest, Pet>().ReverseMap();

        CreateMap<Pet, PetResponse>().ReverseMap();

        CreateMap<PetRequest, PetResponse>().ReverseMap();
    }
}
