﻿using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Models
{
    /// <summary>
    /// DTO za ažuriranje komisije
    /// </summary>
    public class CommissionUpdateDto : IValidatableObject
    {
        /// <summary>
        /// ID komisije
        /// </summary>
        [Required]
        public Guid CommissionID { get; set; }

        /// <summary>
        /// Naziv komisije
        /// </summary>
        [Required(ErrorMessage = "Obavezan je naziv komisije")]
        public string NameCommission { get; set; }

        /// <summary>
        /// Predsednik komisije
        /// </summary>
        public Member President { get; set; }

        /// <summary>
        /// Clanovi komisije
        /// </summary>
        public List<Member> MembersOfCommission { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
