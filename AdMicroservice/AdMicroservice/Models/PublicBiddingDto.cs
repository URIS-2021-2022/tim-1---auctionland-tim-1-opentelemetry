using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Models
{
    public class PublicBiddingDto
    {
        public Guid PublicBiddingId { get; set; }

        public DateTime Date { get; set; }

        public double AuctionedPrice { get; set; }

    }
}
