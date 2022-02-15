using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Models
{
    public class PaymentConfirmationDto
    {
        public Guid PaymentId { get; set; }

        public int AccountNumber { get; set; }

        public int ReferenceNumber { get; set; }

        public DateTime DateOfPayment { get; set; }

        public Guid PublicBiddingId { get; set; }
    }
}
