using System.Collections.Generic;

namespace Pawz.Domain.Entities;

public class Locations
{
    public int Id { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public ICollection<Pets> Pets { get; set; }
}
