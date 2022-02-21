using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Entities
{
    public class CustomerConfirmation
    {
        /// <summary>
        /// ID kupca
        /// </summary>
        [Key]
        public Guid CustomerID { get; set; }

        /// <summary>
        /// Prioritet 
        /// </summary>
        [Required]
        public string Priority { get; set; }

        /// <summary>
        /// Ostvarena povrsina
        /// </summary>
        [Required]
        public string RealizedArea { get; set; }

        /// <summary>
        /// Da li kupac ima zabranu
        /// </summary>
        [Required]
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
        public Guid AddressID { get; set; }

        /// <summary>
        /// Dokumenati kupca
        /// </summary>
        public Guid DocumentsID { get; set; }
    }
}
