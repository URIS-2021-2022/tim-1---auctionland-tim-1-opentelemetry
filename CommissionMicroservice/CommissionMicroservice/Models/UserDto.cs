using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Models
{
    /// <summary>
    /// DTO za korisnika koji moze pristupiti komisiji 
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// ID korisnika
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Ime korisnika
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Prezime korisnika
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Korisnicko ime korisnika 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Lozinka korisnika
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// ID komisije
        /// </summary>
        public Guid CommissionId { get; internal set; }
    }
}
