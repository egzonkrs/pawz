using Pawz.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Pawz.Domain.Entities;

public class Location : IEntity<int>, ISoftDeletion
{
    /// <summary>
    /// The Id of the location
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The city of the location
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// The state or province of the location
    /// </summary>
    public string State { get; set; }

    /// <summary>
    /// The country of the location
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// The postal or ZIP code of the location
    /// </summary>
    public string PostalCode { get; set; }

    /// <summary>
    /// The pets located at this location
    /// </summary>
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();

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
