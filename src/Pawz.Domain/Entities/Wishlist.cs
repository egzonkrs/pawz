using Pawz.Domain.Interfaces;
using System;

namespace Pawz.Domain.Entities;

public class Wishlist : IEntity<int>, ISoftDeletion
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public int PetId { get; set; }
    public Pet Pet { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
