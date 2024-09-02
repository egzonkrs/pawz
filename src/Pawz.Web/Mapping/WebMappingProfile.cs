using AutoMapper;
using Pawz.Application.Models.Pet;
using Pawz.Web.Models;

namespace Pawz.Web.Mapping;

public class WebMappingProfile : Profile
{
    public WebMappingProfile()
    {
        CreateMap<UserPetResponse, UserPetViewModel>().ReverseMap();
        CreateMap<UserPetRequest, UserPetViewModel>().ReverseMap();
    }
}
