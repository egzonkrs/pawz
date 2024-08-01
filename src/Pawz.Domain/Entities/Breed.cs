using System.Collections.Generic;

namespace Pawz.Domain.Entities;

public class Breed
{
    /// <summary>
    /// The Id of the breed
    /// </summary>
    public int Id { get; set; }

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
    public ICollection<Pet> Pets { get; set; }
}
