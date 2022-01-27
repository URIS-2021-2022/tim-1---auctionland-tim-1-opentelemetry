using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Models
{
    public class ApplicationCreationDto
    {
        public Guid ApplicationId { get; set; }

        #region Payment
        [Required(ErrorMessage = "Obavezno je uneti broj uplate")]
        public Guid PaymentId { get; set; }
        #endregion

        #region Buyer
        [Required(ErrorMessage = "Obavezno je uneti uplatioca")]
        public Guid BuyerId { get; set; }
        #endregion
    }
}
