using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models
{
    /// <summary>
    /// Kreiranje klase Principal
    /// </summary>
    public class Principal
    {
        /// <summary>
        /// Korisničko ime korisnika
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// Šifra korisnika 
        /// </summary>
        public string password { get; set; }

    }
}
