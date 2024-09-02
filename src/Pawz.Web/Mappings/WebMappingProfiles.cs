using AutoMapper;
using Pawz.Application.Models;
using Pawz.Web.Models;
using Pawz.Web.Models.Pet;

namespace Pawz.Web.Mappings;

public class WebMappingProfiles : Profile
{
    public WebMappingProfiles()
    {
        CreateMap<PetCreateViewModel, PetCreateRequest>();
    }
}
