﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Entities
{
    public class LeaseAgreement
    {
        [Key]
        public Guid LeaseAgreementID { get; set; }

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
    }
}