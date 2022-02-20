using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Entities
{
    /// <summary>
    /// Predstavlja uplatu prijave za javno nadmetanje.
    /// </summary>
    public class PaymentForApplication
    {
        /// <summary>
        /// ID uplate za javno nadmetanje.
        /// </summary>
        [Key]
        public Guid PaymentId { get; set; }

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
