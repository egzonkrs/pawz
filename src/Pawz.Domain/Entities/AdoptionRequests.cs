using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pawz.Domain.Entities
{
    public class AdoptionRequests
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PetId { get; set; }
        [ForeignKey("PetId")]
        public Pets Pets { get; set; }

        [Required]
        public int RequesterUserId { get; set; }
        [ForeignKey("RequesterUserId")]
        public Users Users { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime? ResponseDate { get; set; } // Nullable nese su vendos hala


    }
}
