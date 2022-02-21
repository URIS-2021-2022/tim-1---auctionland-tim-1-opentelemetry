using PublicBiddingMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicBiddingMicroservice.Models 
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

        [ForeignKey("Status")]
        public Guid? StatusId { get; set; }
        public Status Status { get; set; }

        [ForeignKey("Stage")]
        public Guid? StageId { get; set; }
        public Stage Stage { get; set; }

        [ForeignKey("PublicBiddingType")]
        public Guid? PublicBiddingTypeId { get; set; }
        public Stage PublicBiddingType { get; set; }

    }
}
