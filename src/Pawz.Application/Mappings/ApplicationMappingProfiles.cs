using AutoMapper;
using Pawz.Domain.Entities;
using Pawz.Application.Models.PetModels;
using Pawz.Application.Models.BreedModels;

namespace Pawz.Application.Mappings;
public class ApplicationMappingProfiles : Profile
{
    public ApplicationMappingProfiles()
    {
        CreateMap<PetRequest, Pet>().ReverseMap();
        CreateMap<Pet, PetResponse>().ReverseMap();
        CreateMap<PetRequest, PetResponse>().ReverseMap();

        CreateMap<BreedRequest, Breed>().ReverseMap();
        CreateMap<Breed, BreedResponse>().ReverseMap();
        CreateMap<BreedRequest, BreedResponse>().ReverseMap();
    }
}
