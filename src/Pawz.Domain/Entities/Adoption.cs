using Pawz.Domain.Interfaces;
using System;

namespace Pawz.Domain.Entities;

public class Adoption : IEntity<int>
{
    /// <summary>
    /// The Id of the adoption
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The Id of the adoption request associated with this adoption
    /// </summary>
    public int AdoptionRequestId { get; set; }

    /// <summary>
    /// The adoption request related to this adoption
    /// </summary>
    public AdoptionRequest AdoptionRequest { get; set; }

    /// <summary>
    /// The date when the adoption took place
    /// </summary>
    public DateTime AdoptionDate { get; set; }

    /// <summary>
    /// The fee charged for the adoption
    /// </summary>
    public decimal AdoptionFee { get; set; }
}
