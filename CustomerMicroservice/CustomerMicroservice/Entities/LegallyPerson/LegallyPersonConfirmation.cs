using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Entities
{
    public class LegallyPersonConfirmation : CustomerConfirmation
    {
        /// <summary>
        /// ID pravnog lica
        /// </summary>
        [Key]
        public Guid LegallyPersonID { get; set; }

        /// <summary>
        /// Maticni broj pravnog lica
        /// </summary>
        [Required]
        public string IdentificationNumber { get; set; }

        /// <summary>
        /// Naziv pravnog lica lica
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Faks pravnog lica
        /// </summary>
        [Required]
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
        [Required]
        public string AccountNumber { get; set; }
    }
}
