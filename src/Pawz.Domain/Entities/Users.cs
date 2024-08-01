using System;
using System.Collections.Generic;

namespace Pawz.Domain.Entities;

    public class Users
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Pets> Pets { get; set; }
        public ICollection<AdoptionRequests> AdoptionRequests { get; set; }
        public ICollection<Payments> Payments { get; set; }

    }
