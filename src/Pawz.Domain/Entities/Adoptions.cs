using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pawz.Domain.Entities
{
    public class Adoptions
    {
        public int Id { get; set; }

        [Required]
        public int AdoptionRequestId { get; set; }
        [ForeignKey("AdoptionRequestId")]
        public AdoptionRequests AdoptionRequests { get; set; }

        public DateTime AdoptionDate { get; set; }

        public decimal AdoptionFee { get; set; }

    }
}