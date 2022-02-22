using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models.LegallyPerson
{
    /// <summary>
    /// DTO za modifikaciju pravnog lica
    /// </summary>
    public class LegallyPersonUpdateDto : CustomerUpdateDto
    {
        /// <summary>
        /// ID pravnog lica
        /// </summary>
        //[Required]
        //public Guid LegallyPersonID { get; set; }

        /// <summary>
        /// Maticni broj pravnog lica
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti maticni broj pravnog lica")]
        public string IdentificationNumber { get; set; }

        /// <summary>
        /// Naziv pravnog lica lica
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv pravnog lica")]
        public string Name { get; set; }

        /// <summary>
        /// Faks pravnog lica
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// Broj telefona 1 pravnog lica
        /// </summary>
        public string PhoneNumber1 { get; set; }

        /// <summary>
        /// Broj telefona 2 pravnog lica
        /// </summary>
        public string PhoneNumber2 { get; set; }

        /// <summary>
        /// Email pravnog lica 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Broj racuna pravnog lica
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti broj racuna pravnog lica")]
        public string AccountNumber { get; set; }
    }
}
