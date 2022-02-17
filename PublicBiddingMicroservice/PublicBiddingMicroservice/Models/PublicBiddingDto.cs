using PublicBiddingMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PublicBiddingMicroservice.Models 
{
    public class PublicBiddingUpdateDto
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

        public string Status { get; set; }

        public Guid StageId { get; set; }

    }
}
