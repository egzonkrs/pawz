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
}
