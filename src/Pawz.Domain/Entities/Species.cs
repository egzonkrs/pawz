using System;
using System.Collections.Generic;

namespace Pawz.Domain.Entities;

public class Species
{
    /// <summary>
    /// The Id of the species
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the species
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Description of the species
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The date and time when the species record was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The breeds associated with this species
    /// </summary>
    public ICollection<Breeds> Breeds { get; set; }

    /// <summary>
    /// The pets belonging to this species
    /// </summary>
    public ICollection<Pets> Pets { get; set; }
}
