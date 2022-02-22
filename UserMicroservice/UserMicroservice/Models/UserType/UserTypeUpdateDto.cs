using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Models
{
    /// <summary>
    /// Model za ažuriranje tipa korisnika
    /// </summary>
    public class UserTypeUpdateDto
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
