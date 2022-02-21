using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Models
{
    /// <summary>
    /// DTO za kupca
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// ID kupca
        /// </summary>
        public Guid CustomerID { get; set; }

        /// <summary>
        /// Prioritet 
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// Ostvarena povrsina
        /// </summary>
        public string RealizedArea { get; set; }

        /// <summary>
        /// Da li kupac ima zabranu
        /// </summary>
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
