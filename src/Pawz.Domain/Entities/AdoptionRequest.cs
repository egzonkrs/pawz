using System;

namespace Pawz.Domain.Entities;

public class AdoptionRequest
{
    /// <summary>
    /// The Id of the adoption request
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The Id of the pet associated with this adoption request
    /// </summary>
    public int PetId { get; set; }

    /// <summary>
    /// The status of the adoption request
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// The date when the adoption request was made
    /// </summary>
    public DateTime RequestDate { get; set; }

    /// <summary>
    /// The date when the adoption request was responded to
    /// </summary>
    public DateTime ResponseDate { get; set; }

    /// <summary>
    /// The pet associated with this adoption request
    /// </summary>
    public Pet Pets { get; set; }

    /// <summary>
    /// The adoption related to this request, if applicable
    /// </summary>
    public Adoption Adoption { get; set; }

    /// <summary>
    /// The ID of the user who made the request.
    /// </summary>
    public int RequesterUserId { get; set; }

    /// <summary>
    /// The user who made the request.
    /// </summary>
    public ApplicationUser User { get; set; }
}
