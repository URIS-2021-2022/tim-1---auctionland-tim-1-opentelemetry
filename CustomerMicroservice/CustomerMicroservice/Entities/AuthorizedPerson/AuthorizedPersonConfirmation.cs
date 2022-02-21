using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Entities
{
    public class AuthorizedPersonConfirmation
    {
        /// <summary>
        /// ID ovlascenog lica
        /// </summary>
        [Key]
        public Guid AuthorizedPersonID { get; set; }

        /// <summary>
        /// Ime ovlascenog lica
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Prezime ovlascenog lica
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Broj licnog dokumenta (jmbg ili broj pasosa)
        /// </summary>
        [Required]
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
