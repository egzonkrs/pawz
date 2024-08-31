using AutoMapper;
using Pawz.Application.Models.BreedModels;
using Pawz.Application.Models.PetImagesModels;
using Pawz.Application.Models.PetModels;
using Pawz.Domain.Entities;
using Pawz.Web.Models;

namespace Pawz.Web.Mappings;
public class WebMappingProfiles : Profile
{
    public WebMappingProfiles()
    {
        CreateMap<PetResponse, PetViewModel>().ReverseMap();
        CreateMap<PetRequest, PetViewModel>().ReverseMap();

        CreateMap<Pet, PetViewModel>().ReverseMap(); //I've added this temporarily to solve an automapper issue for now

        CreateMap<BreedResponse, BreedViewModel>().ReverseMap();
        CreateMap<BreedRequest, BreedViewModel>().ReverseMap();

        CreateMap<PetImageResponse, PetImageViewModel>().ReverseMap();
        CreateMap<PetImageRequest, PetImageViewModel>().ReverseMap();

        CreateMap<PetImage, PetImageViewModel>().ReverseMap(); //I've added this temporarily to solve an automapper issue for now
    }
}
