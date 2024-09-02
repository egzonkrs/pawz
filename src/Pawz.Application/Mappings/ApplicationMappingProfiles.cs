using AutoMapper;
using Pawz.Domain.Entities;
using Pawz.Application.Models.PetModels;
using Pawz.Application.Models.BreedModels;
using Pawz.Application.Models.PetImagesModels;
using Pawz.Application.Models.SpeciesModels;
using Pawz.Application.Models.Pet;

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

        CreateMap<PetImageRequest, PetImage>().ReverseMap();
        CreateMap<PetImage, PetImageResponse>().ReverseMap();
        CreateMap<PetImageRequest, PetImageResponse>().ReverseMap();

        CreateMap<SpeciesRequest, Species>().ReverseMap();
        CreateMap<Species, SpeciesResponse>().ReverseMap();
        CreateMap<SpeciesRequest, SpeciesResponse>().ReverseMap();

        CreateMap<UserPetRequest, Pet>().ReverseMap();
        CreateMap<Pet, UserPetResponse>().ReverseMap();
        CreateMap<UserPetRequest, UserPetResponse>().ReverseMap();
    }
}
