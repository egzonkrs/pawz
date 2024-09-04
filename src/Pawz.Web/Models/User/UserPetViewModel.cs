using Pawz.Domain.Enums;
using System;

namespace Pawz.Web.Models.User;

public class UserPetViewModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the pet.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the pet.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the species name of the pet.
    /// </summary>
    public string SpeciesName { get; set; }

    /// <summary>
    /// Gets or sets the breed name of the pet.
    /// </summary>
    public string BreedName { get; set; }

    /// <summary>
    /// Gets or sets the age of the pet in years.
    /// </summary>
    public int AgeYears { get; set; }

    /// <summary>
    /// Gets or sets the age of the pet in months.
    /// </summary>
    public int AgeMonths { get; set; }

    /// <summary>
    /// Gets or sets additional information about the pet.
    /// </summary>
    public string About { get; set; }

    /// <summary>
    /// Gets or sets the price of the pet.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the current status of the pet.
    /// </summary>
    public PetStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the pet was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the name of the location associated with the pet.
    /// </summary>
    public string LocationName { get; set; }

    /// <summary>
    /// Gets or sets the username of the user associated with the pet.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the URL of the pet's photo.
    /// </summary>
    public string PhotoUrl { get; set; }
}
