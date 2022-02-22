using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Entities
{
    public class CommissionConfirmation
    {
        /// <summary>
        /// ID komisije
        /// </summary>
        [Key]
        [Required]
        public Guid CommissionID { get; set; }

        /// <summary>
        /// Naziv komisije
        /// </summary>
        [Required]
        public string NameCommission { get; set; }

        [NotMapped]
        public Member President { get; set; }

        /// <summary>
        /// Clanovi komisije
        /// </summary>
        public List<Member> MembersOfCommission { get; set; }

        /// <summary>
        /// ID korisnika
        /// </summary>
        [ForeignKey("User")]
        public Guid UserId { get; set; }
    }
}
