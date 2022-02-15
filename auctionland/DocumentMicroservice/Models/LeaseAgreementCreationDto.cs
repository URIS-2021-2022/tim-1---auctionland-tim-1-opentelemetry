using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models
{
    public class LeaseAgreementCreationDto : IValidatableObject
    {
        public string LeaseTypeOfGuarantee { get; set; }

        public DateTime LeaseAgreementMaturities { get; set; }

        public DateTime LeaseAgreementEntryDate { get; set; }

        #region Comision
        public Guid MinisterID { get; set; }
        #endregion

        #region PublicBidding
        public Guid PublicBiddingID { get; set; }
        #endregion

        #region Person
        public Guid PersonID { get; set; }
        #endregion

        public DateTime DeadlineForLandRestitution { get; set; }

        public string PlaceOfSigning { get; set; }

        public DateTime DateOfSigning { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(LeaseTypeOfGuarantee == null)
            {
                yield return new ValidationResult(
                   "Nije moguće kreirati ugovor koji nema garantnu osobu.",
                   new[] { "LeaseAgreementCreationDto" });
            }
        }
    }
}
