using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pawz.Domain.Entities
{
    public class PetImages
    {
        public int Id { get; set; }

        [Required]
        public int PetId { get; set; }
        [ForeignKey("PetId")]
        public Pets Pets { get; set; }

        [Display(Name = "Image Url")]
        public string? ImageUrl { get; set; }

        public string Description { get; set; }

        public bool IsPrimary { get; set; }

        public DateTime? UploadedAt { get; set; }
    }
}
