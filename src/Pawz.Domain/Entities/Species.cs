using System;
using System.Collections.Generic;

namespace Pawz.Domain.Entities;

public class Species
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    /// <summary>
    /// The CreatedAt property indicates the date and time when the species record was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    public ICollection<Breeds> Breeds { get; set; }
    public ICollection<Pets> Pets { get; set; }
}
