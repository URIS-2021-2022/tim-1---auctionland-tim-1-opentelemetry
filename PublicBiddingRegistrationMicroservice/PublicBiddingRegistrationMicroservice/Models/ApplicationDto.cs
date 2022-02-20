using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Models
{
    /// <summary>
    /// DTO za prijavu za javno nadmetanje
    /// </summary>
    public class ApplicationDto
    {
        // <summary>
        /// ID prijave za javno nadmetanje.
        /// </summary>
        public Guid ApplicationId { get; set; }

        #region Payment
        /// <summary>
        /// ID uplate za javno nadmetanje.
        /// </summary>
        public Guid PaymentId { get; set; }
        #endregion

        #region Buyer
        /// <summary>
        /// ID kupac na javnom nadmetanju.
        /// </summary>
        public Guid BuyerId { get; set; }
        #endregion
    }
}
