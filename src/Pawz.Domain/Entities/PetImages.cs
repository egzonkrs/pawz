using System;

namespace Pawz.Domain.Entities;

    public class PetImages
    {

        public int Id { get; set; }
        public int PetId { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime UploadedAt { get; set; }
        public Pets Pets { get; set; }

    }
