﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Entities
{
    public class PaymentForApplication
    {
        [Key]
        public Guid PaymentId { get; set; }

        public int AccountNumber { get; set; }

        public int ReferenceNumber { get; set; }

        public string PurposeOfPayment { get; set; }

        public DateTime DateOfPayment { get; set; }

        public Guid PublicBiddingId { get; set; }
    }
}
