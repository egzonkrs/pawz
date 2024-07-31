using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pawz.Domain.Entities
{
    public class Payments
    {
        public int Id { get; set; }

        [Required]
        public int AdoptionId { get; set; }
        [ForeignKey("AdoptionId")]
        public Adoptions Adoptions { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Users Users { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; }

        public string StripePaymentId { get; set; }
    }
}