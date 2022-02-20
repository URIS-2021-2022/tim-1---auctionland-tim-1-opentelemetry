using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Models
{
    /// <summary>
    /// Model za ažuriranje prijave za javno nadmetanje
    /// </summary>
    public class ApplicationUpdateDto
    {
        /// <summary>
        /// ID prijave za javno nadmetanje.
        /// </summary>
        public Guid ApplicationId { get; set; }

        #region Payment
        /// <summary>
        /// ID uplate za javno nadmetanje.
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti broj uplate")]
        public Guid PaymentId { get; set; }
        #endregion

        #region Buyer
        /// <summary>
        /// ID kupca.
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti uplatioca")]
        public Guid BuyerId { get; set; }
        #endregion
    }
}
