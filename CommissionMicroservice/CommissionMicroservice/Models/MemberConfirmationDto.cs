using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Models
{
    /// <summary>
    /// DTO za potvrdu clana komisije
    /// </summary>
    public class MemberConfirmationDto
    {
        /// <summary>
        /// ID clana komisije
        /// </summary>
        [Required]
        public Guid MemberID { get; set; }

        /// <summary>
        /// Ime clana komsije
        /// </summary>
        [Required(ErrorMessage = "Obavezno je ime clana")]
        public string FirstName { get; set; }

        /// <summary>
        /// Prezime clana komisije
        /// </summary>
        [Required(ErrorMessage = "Obavezno je prezime clana")]
        public string LastName { get; set; }

        /// <summary>
        /// Funkcija clana komisije
        /// </summary>
        [Required(ErrorMessage = "Obavezna je funkcija clana")]
        public string Role { get; set; }

        /// <summary>
        /// ID komisije
        /// </summary>
        public Guid CommissionID { get; set; }

        /// <summary>
        /// Naziv komisije
        /// </summary>
        public string NameCommission { get; set; }
    }
}
