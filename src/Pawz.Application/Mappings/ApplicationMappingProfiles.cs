using AutoMapper;
using Pawz.Domain.Entities;
using Pawz.Application.Models.PetModels;
using Pawz.Application.Models.BreedModels;
using Pawz.Application.Models.PetImagesModels;

namespace Pawz.Application.Mappings;
public class ApplicationMappingProfiles : Profile
{
    public ApplicationMappingProfiles()
    {
        CreateMap<PetRequest, Pet>()
            .ForMember(dest => dest.PetImages, opt => opt.MapFrom(src => src.PetImages))
            .ReverseMap();
        CreateMap<Pet, PetResponse>()
            .ForMember(dest => dest.PetImages, opt => opt.MapFrom(src => src.PetImages))
            .ReverseMap();
        CreateMap<PetRequest, PetResponse>()
            .ForMember(dest => dest.PetImages, opt => opt.MapFrom(src => src.PetImages))
            .ReverseMap();

        CreateMap<BreedRequest, Breed>().ReverseMap();
        CreateMap<Breed, BreedResponse>().ReverseMap();
        CreateMap<BreedRequest, BreedResponse>().ReverseMap();

        CreateMap<PetImageRequest, PetImage>().ReverseMap();
        CreateMap<PetImage, PetImageResponse>().ReverseMap();
        CreateMap<PetImageRequest, PetImageResponse>().ReverseMap();
    }
}
