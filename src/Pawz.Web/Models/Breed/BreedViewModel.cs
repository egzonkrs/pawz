using Pawz.Web.Models.Species;

namespace Pawz.Web.Models.Breed;

public class BreedViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SpeciesId { get; set; }
    public SpeciesViewModel Species { get; set; }
}
