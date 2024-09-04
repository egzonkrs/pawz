using AutoMapper;
using Pawz.Application.Models;
using Pawz.Domain.Entities;
using Pawz.Web.Models.Pet;
using Pawz.Web.Models.PetImage;

namespace Pawz.Web.Mappings;

public class WebMappingProfiles : Profile
{
    public WebMappingProfiles()
    {
        CreateMap<PetCreateViewModel, PetCreateRequest>();

        CreateMap<PetImage, PetImageViewModel>().ReverseMap();
    }
}
