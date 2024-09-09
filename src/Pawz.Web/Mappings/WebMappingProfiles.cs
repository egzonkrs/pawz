using AutoMapper;
using Pawz.Application.Models;
using Pawz.Domain.Entities;
using Pawz.Web.Models.Pet;

namespace Pawz.Web.Mappings;

public class WebMappingProfiles : Profile
{
    public WebMappingProfiles()
    {
        CreateMap<PetCreateViewModel, PetCreateRequest>();
        CreateMap<Pet, AdoptionRequestCreateModel>();

        CreateMap<AdoptionRequestCreateModel, AdoptionRequestCreateRequest>();
    }
}
