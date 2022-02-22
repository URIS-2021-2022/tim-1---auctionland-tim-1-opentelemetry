using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Entities
{
    public class UserType
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
