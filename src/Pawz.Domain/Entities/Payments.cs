using System;

namespace Pawz.Domain.Entities;

public class Payments
{
    public int Id { get; set; }
    public int AdoptionId { get; set; }
    public Adoptions Adoptions { get; set; }
    public int UserId { get; set; }
    public Users Users { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string PaymentStatus { get; set; }
    public DateTime PaymentDate { get; set; }
    public string StripePaymentId { get; set; }
}
