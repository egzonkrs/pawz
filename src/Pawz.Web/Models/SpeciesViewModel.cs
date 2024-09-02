using Pawz.Web.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pawz.Web.Models
{
    public class SpeciesViewModel
    {
        [Required(ErrorMessage = "The Id field is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "The Name field must be between 3 and 10 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The CreatedAt field is required.")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "The Breeds collection is required.")]
        [MinCollectionCountValidator(1, ErrorMessage = "The Breeds collection must contain at least one breed.")]
        public ICollection<BreedViewModel> Breeds { get; set; } = new List<BreedViewModel>();

        [Required(ErrorMessage = "The Pets collection is required.")]
        [MinCollectionCountValidator(1, ErrorMessage = "The Pets collection must contain at least one pet.")]
        public ICollection<PetViewModel> Pets { get; set; } = new List<PetViewModel>();
    }
}