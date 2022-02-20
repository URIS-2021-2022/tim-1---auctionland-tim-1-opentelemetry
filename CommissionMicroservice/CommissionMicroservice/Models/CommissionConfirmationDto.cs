﻿using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Models
{
    /// <summary>
    /// DTO za potvrdu komisije
    /// </summary>
    public class CommissionConfirmationDto
    {
        /// <summary>
        /// ID komisije
        /// </summary>
        [Required]
        public Guid CommissionID { get; set; }

        /// <summary>
        /// Naziv komisije
        /// </summary>
        [Required(ErrorMessage = "Obavezan je naziv komsije.")]
        public string NameCommission { get; set; }

        /// <summary>
        /// Predsednik komisije
        /// </summary>
        public Member President { get; set; }

        /// <summary>
        /// Clanovi komisije
        /// </summary>
        public List<Member> MembersOfCommission { get; set; }
    }
}
