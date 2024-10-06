using System.Collections.Generic;

namespace Pawz.Domain.Entities;

public class Wishlist
{
    public required string Id { get; set; }
    public List<Pet> Pets { get; set; } = [];
}
