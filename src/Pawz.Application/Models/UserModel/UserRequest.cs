using Pawz.Application.Models.PetModels;
using System;
using System.Collections.Generic;

namespace Pawz.Application.Models.UserModel;
public class UserRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<PetRequest> Pets { get; set; }

    // public ICollection<AdoptionRequest> AdoptionRequests { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
