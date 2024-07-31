using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pawz.Domain.Entities
{
    public class Pets
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public int SpeciesId { get; set; }
        [ForeignKey("SpeciesId")]
        public Species Species { get; set; }

        [Required]
        public int BreedId { get; set; }
        [ForeignKey("BreedId")]
        public Breeds Breeds { get; set; }

        public int AgeYears { get; set; }
        public int AgeMonths { get; set; }

        public string About { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public int PostedByUserId { get; set; }
        [ForeignKey("PostedByUserId")]
        public Users Users { get; set; }

        [Required]
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Locations Locations { get; set; }


    }
}
