using Pawz.Web.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pawz.Web.Models;

public class PetViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    [StringLength(10, MinimumLength = 3, ErrorMessage = "The Name field must be between 3 and 10 characters.")]
    public string Name { get; set; }

    [StringLength(1000, ErrorMessage = "The About field must be a maximum of 700 characters.")]
    public string About { get; set; }

    [MinCollectionCountValidator(1, ErrorMessage = "The PetImages collection must contain at least one image.")]
    public ICollection<PetImageViewModel> PetImages { get; set; } = new List<PetImageViewModel>();

    [Required(ErrorMessage = "The SpeciesName field is required.")]
    public string SpeciesName { get; set; }

    [Required(ErrorMessage = "The BreedName field is required.")]
    public string BreedName { get; set; }

    [Required(ErrorMessage = "The LocationCity field is required.")]
    public string LocationCity { get; set; }

    [Range(0, 20, ErrorMessage = "The Age in years must be between 0 and 50.")]
    public int AgeYears { get; set; }

    [Range(0, 11, ErrorMessage = "The Age in months must be between 0 and 11.")]
    public int AgeMonths { get; set; }

    [Required(ErrorMessage = "The PostedBy field is required.")]
    public string PostedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Range(0.0, double.MaxValue, ErrorMessage = "The Price must be a positive value.")]
    public decimal Price { get; set; }
}
