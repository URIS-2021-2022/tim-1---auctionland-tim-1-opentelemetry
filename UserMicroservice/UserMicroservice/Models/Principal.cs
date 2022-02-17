using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Models
{
    public class Principal
    {
        /// <summary>
        /// Korisničko ime
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Korisnička lozinka
        /// </summary>
        public string Password { get; set; }
    }
}
