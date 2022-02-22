using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models
{
    /// <summary>
    /// DTO za modifikaciju kupca
    /// </summary>
    public class CustomerUpdateDto
    {
        /// <summary>
        /// ID kupca
        /// </summary>
        [Required]
        public Guid CustomerID { get; set; }

        /// <summary>
        /// Da li je kupac fizicko lice, ako nije onda je pravno
        /// </summary>
        public bool IsPhysicalPerson { get; set; }

        /// <summary>
        /// Prioritet 
        /// </summary>
        [Required(ErrorMessage = "Mora se uneti prioritet")]
        public string Priority { get; set; }

        /// <summary>
        /// Ostvarena povrsina
        /// </summary>
        [Required(ErrorMessage = "Potrebno je uneti ostvarenu povrsinu kupca")]
        public string RealizedArea { get; set; }

        /// <summary>
        /// Da li kupac ima zabranu
        /// </summary>
        [Required(ErrorMessage = "Unesite da li kupac ima zabranu")]
        public bool HasABan { get; set; }

        /// <summary>
        /// Datum pocetka zabrane
        /// </summary>
        public DateTime StartDateBan { get; set; }

        /// <summary>
        /// Duzina trajanja zabrane
        /// </summary>
        public int DurationBan { get; set; }

        /// <summary>
        /// Datum prestanka zabrane
        /// </summary>
        public DateTime EndDateBan { get; set; }

        /// <summary>
        /// Adresa kupca
        /// </summary>
        [ForeignKey("Address")]
        public Guid AddressId { get; set; }

        /// <summary>
        /// Dokumenti kupca
        /// </summary>
        [ForeignKey("Document")]
        public Guid DocumentID { get; set; }
    }
}
