using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Models
{
    /// <summary>
    /// DTO za korisnika
    /// </summary>
    public class UserTypeDto
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
