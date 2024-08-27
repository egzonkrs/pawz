using Pawz.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Pawz.Domain.Entities;

public class City : IEntity<int>, ISoftDeletion
{
    /// <summary>
    /// The Id of the city.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the city.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The Id of the country to which this city belongs.
    /// </summary>
    public int CountryId { get; set; }

    /// <summary>
    /// The country to which this city belongs.
    /// </summary>
    public Country Country { get; set; }

    /// <summary>
    /// The collection of locations within this city.
    /// </summary>
    public ICollection<Location> Locations { get; set; } = new List<Location>();

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
