using AutoMapper;
using Pawz.Domain.Entities;
using Pawz.Application.Models.Pet;
using System.Linq;

namespace Pawz.Application.Mappings;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        CreateMap<Pet, PetResponse>()
            .ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.Species.Name))
            .ForMember(dest => dest.Breed, opt => opt.MapFrom(src => src.Breed.Name))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location.City))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.PetImages, opt => opt.MapFrom(src => src.PetImages.Select(img => img.ImageUrl).ToList()));

        CreateMap<Pet, PetRequest>()
            .ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.Species.Name))
            .ForMember(dest => dest.Breed, opt => opt.MapFrom(src => src.Breed.Name))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location.City))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.PetImages, opt => opt.MapFrom(src => src.PetImages.Select(img => img.ImageUrl).ToList()));

        CreateMap<PetResponse, Pet>()
            .ForMember(dest => dest.Species, opt => opt.Ignore())
            .ForMember(dest => dest.Breed, opt => opt.Ignore())
            .ForMember(dest => dest.Location, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.PetImages, opt => opt.Ignore());

        CreateMap<PetRequest, Pet>()
            .ForMember(dest => dest.Species, opt => opt.Ignore())
            .ForMember(dest => dest.Breed, opt => opt.Ignore())
            .ForMember(dest => dest.Location, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.PetImages, opt => opt.Ignore());
    }
}
