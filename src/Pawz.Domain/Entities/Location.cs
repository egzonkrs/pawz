using Pawz.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Pawz.Domain.Entities;

public class Location : IEntity<int>, ISoftDeletion
{
    /// <summary>
    /// The Id of the location.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The Id of the city where the location is situated.
    /// </summary>
    public int CityId { get; set; }

    /// <summary>
    /// The city where the location is situated.
    /// </summary>
    public City City { get; set; }

    /// <summary>
    /// The specific address of the location, such as street name or house number.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// The postal or ZIP code of the location.
    /// </summary>
    public string PostalCode { get; set; }

    /// <summary>
    /// The collection of pets associated with this location.
    /// </summary>
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();

    /// <summary>
    /// Indicates whether the entity is soft-deleted.
    /// This property is implemented from the <see cref="ISoftDeletion"/> interface.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// The timestamp of when the entity was soft-deleted.
    /// This property is implemented from the <see cref="ISoftDeletion"/> interface.
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }
}
