using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Entities
{
    /// <summary>
    /// Predstavlja prijavu za javno nadmetanje
    /// </summary>
    public class ApplicationForPublicBidding
    {
        /// <summary>
        /// ID prijave za javno nadmetanje.
        /// </summary>
        [Key]
        public Guid ApplicationId { get; set; }

        #region Payment
        /// <summary>
        /// ID uplate za javno nadmetanje.
        /// </summary>
        public Guid PaymentId { get; set; }
        #endregion

        #region Buyer
        /// <summary>
        /// ID kupca.
        /// </summary>
        public Guid BuyerId { get; set; }
        #endregion
    }
}
