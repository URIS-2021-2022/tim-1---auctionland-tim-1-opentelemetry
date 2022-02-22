using DocumentMsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.ServiceCalls.PublicBiddingMicroservice
{
    public interface IPublicBiddingMicroservice
    {
        public bool GetPublicBiddings(PublicBiddingDto publicBiddingDto);
    }
}
