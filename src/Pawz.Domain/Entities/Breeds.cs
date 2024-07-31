using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pawz.Domain.Entities
{
    public class Breeds
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SpeciesId { get; set; }
        [ForeignKey("SpeciesId")]
        public Species Species { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
