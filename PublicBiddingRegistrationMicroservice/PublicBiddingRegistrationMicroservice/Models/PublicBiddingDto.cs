﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Models
{
    /// <summary>
    /// DTO za javno nadmetanje.
    /// </summary>
    public class PublicBiddingDto
    {
        public Guid PublicBiddingId { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double StartingPricePerHe { get; set; }

        public bool Exclude { get; set; }

        public double AuctionedPrice { get; set; }

        public int LeasePeriod { get; set; }

        public int NumberOfParticipants { get; set; }

        public double DepositReplenishment { get; set; }

        public int Circle { get; set; }

        public Guid? StatusId { get; set; }

        public Guid? StageId { get; set; }

        public Guid? PublicBiddingTypeId { get; set; }

        public Guid AddressId { get; set; }
    }
}
