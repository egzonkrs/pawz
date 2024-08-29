using AutoMapper;
using Pawz.Application.Models.Pet;
using Pawz.Web.Models;

namespace Pawz.Web.Mappings;

public class WebMappingProfiles : Profile
{
    public WebMappingProfiles()
    {
        CreateMap<PetResponse, PetViewModel>().ReverseMap();
        CreateMap<PetRequest, PetViewModel>().ReverseMap();
    }
}
