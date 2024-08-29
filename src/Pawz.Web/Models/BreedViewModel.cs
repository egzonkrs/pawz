using System.Collections.Generic;

namespace Pawz.Web.Models
{
    public class BreedViewModel
    {
        public int Id { get; set; }
        public int SpeciesId { get; set; }
        public SpeciesViewModel Species { get; set; }
        public string Name { get; set; }
        public ICollection<PetViewModel> Pets { get; set; } = new List<PetViewModel>();
    }
}