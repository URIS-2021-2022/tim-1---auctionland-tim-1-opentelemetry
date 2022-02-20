using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Models
{
    /// <sumary>
    /// DTO za clana komisije
    /// </sumary>
    public class MemberDto
    {
        /// <summary>
        /// ID clana komisije
        /// </summary>
        [Required]
        public Guid MemberID { get; set; }

        /// <summary>
        /// Ime clana komisije
        /// </summary>
        [Required(ErrorMessage = "Obavezno je ime clana komsije")]
        public string FirstName { get; set; }

        /// <summary>
        /// Prezime clana komisije
        /// </summary>
        [Required(ErrorMessage = "Obavezno je prezime clana komisije")]
        public string LastName { get; set; }

        /// <summary>
        /// Funkcija clana komisije
        /// </summary>
        [Required(ErrorMessage = "Obavezna je funkcija clana komisije")]
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
