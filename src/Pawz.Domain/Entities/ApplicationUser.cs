using Microsoft.AspNetCore.Identity;
using Pawz.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Pawz.Domain.Entities;

public class ApplicationUser : IdentityUser, ISoftDeletion
{
    /// <summary>
    /// The user's first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// The user's last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// The user's address.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// The user's address.
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// The date and time when the user was created.
    /// Default is the current date and time.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// A collection of pets owned by the user.
    /// </summary>
    public ICollection<Pet> Pets { get; set; }

    /// <summary>
    /// A collection of adoption requests made by the user.
    /// </summary>
    public ICollection<AdoptionRequest> AdoptionRequests { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity is soft-deleted.
    /// This property is implemented from the <see cref="ISoftDelete"/> interface.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the entity was soft-deleted.
    /// This property is implemented from the <see cref="ISoftDelete"/> interface.
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }
}
