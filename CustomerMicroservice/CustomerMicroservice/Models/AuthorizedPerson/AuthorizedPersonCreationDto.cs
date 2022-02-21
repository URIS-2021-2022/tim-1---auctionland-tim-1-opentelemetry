using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models.AuthorizedPerson
{
    /// <summary>
    /// DTO za kreiranje ovlascenog lica
    /// </summary>
    public class AuthorizedPersonCreationDto
    {
        /// <summary>
        /// ID ovlascenog lica
        /// </summary>
        [Required]
        public Guid AuthorizedPersonID { get; set; }

        /// <summary>
        /// Ime ovlascenog lica
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti ime ovlascenog lica")]
        public string FirstName { get; set; }

        /// <summary>
        /// Prezime ovlascenog lica
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti prezime ovlascenog lica")]
        public string LastName { get; set; }

        /// <summary>
        /// Broj licnog dokumenta (jmbg ili broj pasosa)
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti broj licnog dokumenta ovlascenog lica")]
        public string NumberPersonalDocument { get; set; }

        /// <summary>
        /// Broj table
        /// </summary>
        public string BoardTable { get; set; }

        /// <summary>
        /// Adresa ovlascenog lica
        /// </summary>
        //public Guid AddressID { get; set; }
    }
}
