using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Entities
{
    public class User
    {
        /// <summary>
        /// Id korisnika
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
        /// Korisničko ime
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email korisnika
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Hash-ovana šifra korisnika
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Salt
        /// </summary>
        public string Salt { get; set; }
    }
}
