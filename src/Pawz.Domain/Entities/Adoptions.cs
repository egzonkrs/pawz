using System;

namespace Pawz.Domain.Entities;

    public class Adoptions
    {
        public int Id { get; set; }
        public int AdoptionRequestId { get; set; }
        public AdoptionRequests AdoptionRequests { get; set; }
        public DateTime AdoptionDate { get; set; }
        public decimal AdoptionFee { get; set; }
        public Payments Payments { get; set; }
    }
