using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Entities
{
    /// <summary>
    /// Predstavlja listu dokumenata
    /// </summary>
    public class LeaseAgreement
    {
        /// <summary>
        /// ID ugovora o zakupu.
        /// </summary>
        [Key]
        public Guid LeaseAgreementID { get; set; }

        /// <summary>
        /// Tip garancije.
        /// </summary>
        public string LeaseTypeOfGuarantee { get; set; }

        /// <summary>
        /// Rokovi dospeća.
        /// </summary>
        public DateTime LeaseAgreementMaturities { get; set; }

        /// <summary>
        /// Datum zavođenja.
        /// </summary>
        public DateTime LeaseAgreementEntryDate { get; set; }

        #region Comision
        /// <summary>
        /// ID ministra komisije.
        /// </summary>
        public Guid MinisterID { get; set; }
        #endregion

        #region PublicBidding
        /// <summary>
        /// ID javnog nadmetanja.
        /// </summary>
        public Guid PublicBiddingID { get; set; }
        #endregion

        #region Person
        /// <summary>
        /// ID lica.
        /// </summary>
        public Guid PersonID { get; set; }
        #endregion

        /// <summary>
        /// Rok za vraćanje zemljišta.
        /// </summary>
        public DateTime DeadlineForLandRestitution { get; set; }

        /// <summary>
        /// Mesto potpisivanja.
        /// </summary>
        public string PlaceOfSigning { get; set; }

        /// <summary>
        /// Datum potpisivanja.
        /// </summary>
        public DateTime DateOfSigning { get; set; }
    }
}
