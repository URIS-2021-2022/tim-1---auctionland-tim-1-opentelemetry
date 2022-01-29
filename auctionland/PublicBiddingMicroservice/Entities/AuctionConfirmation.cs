using PublicBiddingMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.Entities
{
    public class AuctionConfirmation
    {
        public Guid AuctionId { get; set; }

        public int Number { get; set; }

        public int Year { get; set; }

        public DateTime Date { get; set; }

        public double PriceIncrease { get; set; }

        public DateTime ApplicationDeadline { get; set; }

    }
}
