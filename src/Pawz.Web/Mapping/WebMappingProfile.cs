using AutoMapper;
using Pawz.Application.Models;
using Pawz.Application.Models.BreedModels;
using Pawz.Application.Models.Pet;
using Pawz.Application.Models.PetImagesModels;
using Pawz.Application.Models.PetModels;
using Pawz.Application.Models.SpeciesModels;
using Pawz.Domain.Entities;
using Pawz.Web.Models;
using Pawz.Web.Models.Breed;
using Pawz.Web.Models.Location;
using Pawz.Web.Models.Pet;
using Pawz.Web.Models.PetImage;
using Pawz.Web.Models.Species;
using Pawz.Web.Models.User;

namespace Pawz.Web.Mapping;

public class WebMappingProfile : Profile
{
    public WebMappingProfile()
    {
        CreateMap<UserPetResponse, UserPetViewModel>().ReverseMap();
        CreateMap<UserPetRequest, UserPetViewModel>().ReverseMap();

        CreateMap<PetResponse, PetViewModel>().ReverseMap();
        CreateMap<PetRequest, PetViewModel>().ReverseMap();

        CreateMap<PetCreateViewModel, PetCreateRequest>().ReverseMap();

        CreateMap<Pet, PetViewModel>().ReverseMap(); //I've added this temporarily to solve an automapper issue for now

        CreateMap<BreedResponse, BreedViewModel>().ReverseMap();
        CreateMap<BreedRequest, BreedViewModel>().ReverseMap();

        CreateMap<Breed, BreedViewModel>().ReverseMap(); //I've added this temporarily to solve an automapper issue for now

        CreateMap<PetImageResponse, PetImageViewModel>().ReverseMap();
        CreateMap<PetImageRequest, PetImageViewModel>().ReverseMap();

        CreateMap<PetImage, PetImageViewModel>().ReverseMap(); //I've added this temporarily to solve an automapper issue for now

        CreateMap<SpeciesResponse, SpeciesViewModel>().ReverseMap();
        CreateMap<SpeciesRequest, SpeciesViewModel>().ReverseMap();

        CreateMap<Species, SpeciesViewModel>().ReverseMap(); //I've added this temporarily to solve an automapper issue for now

        CreateMap<ApplicationUser, UserViewModel>().ReverseMap(); //I've added this temporarily to solve an automapper issue for now

        CreateMap<Location, LocationViewModel>().ReverseMap(); //I've added this temporarily to solve an automapper issue for now

        CreateMap<City, CityViewModel>().ReverseMap(); //I've added this temporarily to solve an automapper issue for now

        CreateMap<Country, CountryViewModel>().ReverseMap(); //I've added this temporarily to solve an automapper issue for now

        CreateMap<AdoptionRequestResponse, AdoptionRequestViewModel>().ReverseMap();

    }
}
