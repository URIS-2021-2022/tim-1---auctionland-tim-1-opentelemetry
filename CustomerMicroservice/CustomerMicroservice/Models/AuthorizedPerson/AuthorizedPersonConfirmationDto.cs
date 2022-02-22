using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models.AuthorizedPerson
{
    /// <summary>
    /// DTO za potvrdjivanje ovlascenog lica
    /// </summary>
    public class AuthorizedPersonConfirmationDto
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
        [ForeignKey("Address")]
        public Guid AddressId { get; set; }
    }
}
