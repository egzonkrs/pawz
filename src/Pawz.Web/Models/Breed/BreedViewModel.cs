using Pawz.Web.Models.Species;

namespace Pawz.Web.Models.Breed;

public class BreedViewModel
{
    /// <summary>
    /// The Id of the breed
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the breed.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The Id of the species to which this breed belongs
    /// </summary>
    public int SpeciesId { get; set; }

    /// <summary>
    /// Gets or sets the species information for the pet.
    /// This property represents the species to which the pet belongs, 
    /// and is encapsulated in a SpeciesViewModel object.
    /// </summary>
    public SpeciesViewModel Species { get; set; }
}
