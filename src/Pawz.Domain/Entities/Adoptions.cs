using System;

namespace Pawz.Domain.Entities;

public class Adoptions
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
    public AdoptionRequests AdoptionRequests { get; set; }

    /// <summary>
    /// The date when the adoption took place
    /// </summary>
    public DateTime AdoptionDate { get; set; }

    /// <summary>
    /// The fee charged for the adoption
    /// </summary>
    public decimal AdoptionFee { get; set; }

    /// <summary>
    /// The payment details for the adoption
    /// </summary>
    public Payments Payments { get; set; }
}
