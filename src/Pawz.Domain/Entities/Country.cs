using Pawz.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Pawz.Domain.Entities;

public class Country : IEntity<int>, ISoftDeletion
{
    /// <summary>
    /// The Id of the country.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the country.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The collection of cities within this country.
    /// </summary>
    public ICollection<City> Cities { get; set; } = new List<City>();

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
