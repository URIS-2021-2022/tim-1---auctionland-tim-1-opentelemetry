using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Entities
{
    public class Member
    {
        /// <summary>
        /// ID clana komisije
        /// </summary>
        [Key]
        [Required]
        public Guid MemberID { get; set; }

        /// <summary>
        /// Ime clana komisije
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Prezime clana komisije
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Funkcija clana komisije
        /// </summary>
        [Required]
        public string Role { get; set; }

        /// <summary>
        /// Identifikator komisije
        /// </summary>
        [ForeignKey("CommissionID")]
        public Guid CommissionID { get; set; }
        public string NameCommission { get; set; }
    }
}
