using Pawz.Web.Models.Pet;
using Pawz.Web.Models.User;
using System;
using System.Collections.Generic;

namespace Pawz.Web.Models.Wishlist;

public class WishlistViewModel
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public UserViewModel User { get; set; }
    public List<PetViewModel> Pets { get; set; } = new List<PetViewModel>();
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
