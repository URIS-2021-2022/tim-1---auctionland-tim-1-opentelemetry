using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Entities
{
    public class User
    {
        /// <summary>
        /// ID korisnika 
        /// </summary>
        public Guid Id { get; set; }

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
        /// Mejl korisnika
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Lozinka korisnika
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Salt 
        /// </summary>
        public string Salt { get; set; }
    }
}
