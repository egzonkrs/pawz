using Pawz.Domain.Entities;
using Pawz.Web.Models.Pet;
using System;
using System.Collections.Generic;

namespace Pawz.Web.Models.User;

public class UserViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Address { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<PetViewModel> Pets { get; set; }
    public ICollection<AdoptionRequest> AdoptionRequests { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
