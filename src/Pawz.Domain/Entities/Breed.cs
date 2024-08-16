using Pawz.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Pawz.Domain.Entities;

public class Breed : IEntity<int>, ISoftDeletion
{
    /// <summary>
    /// The Id of the breed
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The Id of the species to which this breed belongs
    /// </summary>
    public int SpeciesId { get; set; }

    /// <summary>
    /// The species to which this breed belongs
    /// </summary>
    public Species Species { get; set; }

    /// <summary>
    /// The name of the breed
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Description of the breed
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The pets of this breed
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
