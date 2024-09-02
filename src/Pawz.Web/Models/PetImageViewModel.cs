using System;
using System.ComponentModel.DataAnnotations;

namespace Pawz.Web.Models;
public class PetImageViewModel
{
    [Required(ErrorMessage = "The Id field is required.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "The PetId field is required.")]
    public int PetId { get; set; }

    [Required(ErrorMessage = "The Pet field is required.")]
    public PetViewModel Pet { get; set; }

    [Required(ErrorMessage = "The Image URL is required.")]
    public string ImageUrl { get; set; }

    [StringLength(500, ErrorMessage = "The description must be less than 500 characters.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "The IsPrimary field is required.")]
    public bool IsPrimary { get; set; }

    [Required(ErrorMessage = "The UploadedAt field is required.")]
    public DateTime UploadedAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}