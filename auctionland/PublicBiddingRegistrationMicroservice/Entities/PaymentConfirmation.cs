using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Entities
{
    public class PaymentConfirmation
    {
        public int AccountNumber { get; set; }

        public int ReferenceNumber { get; set; }

        public string PurposeOfPayment { get; set; }

        public DateTime DateOfPayment { get; set; }

        public Guid PublicBiddingId { get; set; }
    }
}
