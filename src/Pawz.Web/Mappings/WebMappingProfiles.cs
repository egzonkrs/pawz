using AutoMapper;
using Pawz.Application.Models;
using Pawz.Web.Models;

namespace Pawz.Web.Mappings;

public class WebMappingProfiles : Profile
{
    public WebMappingProfiles()
    {
        CreateMap<PetCreateViewModel, PetCreateRequest>();
    }
}
