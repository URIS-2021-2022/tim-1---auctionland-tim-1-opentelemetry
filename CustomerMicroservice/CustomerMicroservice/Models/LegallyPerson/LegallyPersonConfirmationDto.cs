using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models.LegallyPerson
{
    /// <summary>
    /// DTO za potvrdjivanje pravnog lica
    /// </summary>
    public class LegallyPersonConfirmationDto : CustomerConfirmationDto
    {
        /// <summary>
        /// ID pravnog lica
        /// </summary>
        public Guid LegallyPersonID { get; set; }

        /// <summary>
        /// Maticni broj pravnog lica
        /// </summary>
        public string IdentificationNumber { get; set; }

        /// <summary>
        /// Naziv pravnog lica lica
        /// </summary>
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
        public string AccountNumber { get; set; }
    }
}
