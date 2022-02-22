using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Models
{
    /// <summary>
    /// Model za kreiranje tipa korisnika
    /// </summary>
    public class UserTypeCreationDto
    {
        /// <summary>
        /// Id tipa korisnika
        /// </summary>
        public Guid UserTypeId { get; set; }

        /// <summary>
        /// Naziv tipa korisnika
        /// </summary>
        public string UserTypeName { get; set; }

    }
}
