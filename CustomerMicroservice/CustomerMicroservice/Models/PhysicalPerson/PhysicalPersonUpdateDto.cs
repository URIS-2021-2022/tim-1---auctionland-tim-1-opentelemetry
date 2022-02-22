using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models.PhysicalPerson
{
    /// <summary>
    /// DTO za modifikaciju fizickog lica
    /// </summary>
    public class PhysicalPersonUpdateDto : CustomerUpdateDto
    {
        /// <summary>
        /// ID fizickog lica
        /// </summary>
        //public Guid PhysicalPersonID { get; set; }

        /// <summary>
        /// JMBG fizickog lica
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti JMBG fizickog lica")]
        public string JMBG { get; set; }

        /// <summary>
        /// Ime fizickog lica
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti ime fizickog lica")]
        public string FirstName { get; set; }

        /// <summary>
        /// Prezime fizickog lica
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti prezime fizickog lica")]
        public string LastName { get; set; }

        /// <summary>
        /// Broj telefona 1 fizickog lica
        /// </summary>
        public string PhoneNumber1 { get; set; }

        /// <summary>
        /// Broj telefona 2 fizickog lica
        /// </summary>
        public string PhoneNumber2 { get; set; }

        /// <summary>
        /// Email fizickog lica 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Broj racuna fizickog lica
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti broj racuna fizickog lica")]
        public string AccountNumber { get; set; }
    }
}
