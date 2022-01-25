﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
    public class LeaseAgreement
    {
        public Guid LeaseAgreementID { get; set; }

        public string LeaseTypeOfGuarantee { get; set; }

        public DateTime LeaseAgreementMaturities { get; set; }

        public DateTime LeaseAgreementEntryDate { get; set; }

        //public Komisija ministerID { get; set; }

        //public PublicBidding PublicBiddingID { get; set; }

        //public Lice LiceID { get; set; }

        public DateTime DeadlineForLandRestitution { get; set; }

        public string PlaceOfSigning { get; set; }

        public DateTime DateOfSigning { get; set; }
    }
}
