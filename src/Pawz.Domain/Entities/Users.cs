using System;
using System.ComponentModel.DataAnnotations;


namespace Pawz.Domain.Entities
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Role { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}
