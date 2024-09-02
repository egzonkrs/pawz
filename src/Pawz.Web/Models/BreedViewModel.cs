using Pawz.Web.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pawz.Web.Models
{
    public class BreedViewModel
    {
        [Required(ErrorMessage = "The Id field is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The SpeciesId field is required.")]
        public int SpeciesId { get; set; }

        [Required(ErrorMessage = "The Species field is required.")]
        public SpeciesViewModel Species { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "The Name field must be between 3 and 10 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Pets collection is required.")]
        [MinCollectionCountValidator(1, ErrorMessage = "The Pets collection must contain at least one image.")]
        public ICollection<PetViewModel> Pets { get; set; } = new List<PetViewModel>();
    }
}