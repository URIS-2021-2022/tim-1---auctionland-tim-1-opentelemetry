using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Entities
{
    public class ApplicationForPublicBidding
    {
        [Key]
        public Guid ApplicationId { get; set; }

        #region Payment
        public Guid PaymentId { get; set; }
        #endregion

        #region Buyer
        public Guid BuyerId { get; set; }
        #endregion
    }
}
