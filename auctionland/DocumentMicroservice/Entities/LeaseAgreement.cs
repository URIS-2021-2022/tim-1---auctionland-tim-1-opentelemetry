using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentMicroservice.Entities
{
    public class LeaseAgreement
    {
        [Key]
        public Guid LeaseAgreementID { get; set; }

        [Column("nvarchar(50)")]
        public string LeaseTypeOfGuarantee { get; set; }

        [Column("datetime")]
        public DateTime LeaseAgreementMaturities { get; set; }

        [Column("datetime")]
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

        [Column("datetime")]
        public DateTime DeadlineForLandRestitution { get; set; }

        [Column("nvarchar(100)")]
        public string PlaceOfSigning { get; set; }

        [Column("datetime")]
        public DateTime DateOfSigning { get; set; }
    }
}
