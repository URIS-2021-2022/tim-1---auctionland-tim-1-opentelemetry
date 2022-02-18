using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{
    /// <summary>
    /// Model za autentifikaciju
    /// </summary>
    public class Principal
    {
        /// <summary>
        /// Korisnicko ime
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Korisnička lozinka
        /// </summary>
        public string Password { get; set; }
    }
}
