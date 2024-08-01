using System;
using System.ComponentModel.DataAnnotations;

namespace Pawz.Domain.Entities;

    public class AdoptionRequests
    {

        public int Id { get; set; }
        public int PetId { get; set; }
        public int RequesterUserId { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ResponseDate { get; set; }
        public Pets Pets { get; set; }
        public Users Users { get; set; }
        public Adoptions Adoptions { get; set; }

    }
