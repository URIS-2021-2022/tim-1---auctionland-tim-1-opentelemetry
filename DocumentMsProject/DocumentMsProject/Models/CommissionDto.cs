using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Models
{
    /// <summary>
    /// DTO za clana komisije
    /// </summary>
    public class CommissionDto
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
