﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Models
{
    /// <summary>
    /// DTO za tip javnog nadmetanja.
    /// </summary>
    public class PublicBiddingTypeConfirmationDto
    {
        /// <summary>
        /// Id tipa javnog nadmetanja
        /// </summary> 
        public Guid PublicBiddingTypeId { get; set; }

        /// <summary>
        /// Naziv tipa javnog nadmetanja
        /// </summary> 
        public string PublicBiddingTypeName { get; set; }
    }
}
