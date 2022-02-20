﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Models
{
    /// <summary>
    /// Model za kreiranje uplate za javno nadmetanje
    /// </summary>
    public class PaymentCreationDto
    {
        /// <summary>
        /// Broj računa.
        /// </summary>
        public int AccountNumber { get; set; }

        /// <summary>
        /// Poziv na broj.
        /// </summary>
        public int ReferenceNumber { get; set; }

        /// <summary>
        /// Svrha uplate.
        /// </summary>
        public string PurposeOfPayment { get; set; }

        /// <summary>
        /// Datum uplate.
        /// </summary>
        public DateTime DateOfPayment { get; set; }

        /// <summary>
        /// ID javnog nadmetanja.
        /// </summary>
        public Guid PublicBiddingId { get; set; }
    }
}
