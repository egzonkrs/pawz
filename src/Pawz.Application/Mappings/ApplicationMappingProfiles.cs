using AutoMapper;
using Pawz.Application.Models;
using Pawz.Application.Models.BreedModels;
using Pawz.Application.Models.NotificationModels;
using Pawz.Application.Models.Pet;
using Pawz.Application.Models.PetImagesModels;
using Pawz.Application.Models.PetModels;
using Pawz.Application.Models.SpeciesModels;
using Pawz.Domain.Entities;

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

        CreateMap<PetCreateRequest, Pet>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<UserPetRequest, Pet>().ReverseMap();
        CreateMap<Pet, UserPetResponse>().ReverseMap();
        CreateMap<UserPetRequest, UserPetResponse>().ReverseMap();

        CreateMap<AdoptionRequest, AdoptionRequestResponse>().ReverseMap();

        CreateMap<NotificationRequest, Notification>();
        CreateMap<Notification, NotificationResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));


    }
}
