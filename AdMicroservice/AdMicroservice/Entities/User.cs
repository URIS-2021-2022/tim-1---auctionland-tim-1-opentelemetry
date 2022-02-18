using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities
{
    /// <summary>
    /// Kreirana klasa korisnik
    /// </summary>
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
        /// Korisničko ime korisnika 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Mejl korisnika
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Šifra korisnika 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Privatni ključ korisnika 
        /// </summary>
        public string Salt { get; set; }
        
    }
}
