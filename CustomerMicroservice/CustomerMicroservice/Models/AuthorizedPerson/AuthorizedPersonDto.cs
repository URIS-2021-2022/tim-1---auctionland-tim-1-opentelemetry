using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models.AuthorizedPerson
{
    /// <summary>
    /// DTO za ovlasceno lice
    /// </summary>
    public class AuthorizedPersonDto
    {
        /// <summary>
        /// ID ovlascenog lica
        /// </summary>
        public Guid AuthorizedPersonID { get; set; }

        /// <summary>
        /// Ime ovlascenog lica
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Prezime ovlascenog lica
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Broj licnog dokumenta (jmbg ili broj pasosa)
        /// </summary>
        public string NumberPersonalDocument { get; set; }

        /// <summary>
        /// Broj table
        /// </summary>
        public string BoardTable { get; set; }

        /// <summary>
        /// Adresa ovlascenog lica
        /// </summary>
        public Guid AddressID { get; set; }
    }
}
