using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Models
{
    /// <summary>
    /// Model za kreiranje ugovora o zakupu
    /// </summary>
    public class LeaseAgreementCreationDto
    {
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
