using System;

namespace Pawz.Domain.Entities;

public class Payments
{
    /// <summary>
    /// The Id of the payment
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The Id of the adoption associated with this payment
    /// </summary>
    public int AdoptionId { get; set; }

    /// <summary>
    /// The adoption related to this payment
    /// </summary>
    public Adoptions Adoptions { get; set; }

    /// <summary>
    /// The amount of the payment
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// The currency of the payment
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// The status of the payment
    /// </summary>
    public string PaymentStatus { get; set; }

    /// <summary>
    /// The date and time when the payment was made
    /// </summary>
    public DateTime PaymentDate { get; set; }

    /// <summary>
    /// The Stripe payment identifier
    /// </summary>
    public string StripePaymentId { get; set; }
}
