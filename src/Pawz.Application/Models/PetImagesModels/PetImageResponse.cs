using Pawz.Application.Models.PetModels;
using System;

namespace Pawz.Application.Models.PetImagesModels
{
    public class PetImageResponse
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public PetResponse Pet { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime UploadedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}