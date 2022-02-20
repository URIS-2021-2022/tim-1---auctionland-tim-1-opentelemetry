using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Models
{
    /// <summary>
    /// Model za kreiranje prijave za javno nadmetanje
    /// </summary>
    public class ApplicationCreationDto : IValidatableObject
    {
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaymentId == BuyerId)
            {
                yield return new ValidationResult(
                   "Nije moguće kreirati prijavu u kojoj se jednakom vrednošću u sistemu identifikuju i kupac i uplata.",
                   new[] { "ApplicationCreationDto" });
            }
        }
        #endregion
    }
}
