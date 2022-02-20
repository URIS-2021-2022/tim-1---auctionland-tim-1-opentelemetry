using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Models
{
    /// <summary>
    /// Predstavlja model za autentifikaciju
    /// </summary>
    public class Principal
    {
        /// <summary>
        /// Korisničko ime
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Korisničko lozinka
        /// </summary>
        public string Password { get; set; }
    }
}
