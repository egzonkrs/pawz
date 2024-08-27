using AutoMapper;
using Pawz.Domain.Entities;
using Pawz.Application.Models.Pet;

namespace Pawz.Application.Mappings;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PetRequest, Pet>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.SpeciesName))
            .ForMember(dest => dest.Breed, opt => opt.Ignore())
            .ForMember(dest => dest.Location, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.PetImages, opt => opt.Ignore())
            .ForMember(dest => dest.AdoptionRequests, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Pet, PetResponse>()
            .ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.Species.Name))
            .ForMember(dest => dest.Breed, opt => opt.MapFrom(src => src.Breed.Name))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => $"{src.Location.City}, {src.Location.State}, {src.Location.Country}, {src.Location.PostalCode}"))
            .ReverseMap();
    }
}
