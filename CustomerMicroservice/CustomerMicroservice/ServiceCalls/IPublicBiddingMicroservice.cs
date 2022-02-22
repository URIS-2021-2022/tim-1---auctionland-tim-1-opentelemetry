using CustomerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.ServiceCalls
{
    public interface IPublicBiddingMicroservice
    {
        public Task<PublicBiddingDto> GetPublicBiddings(Guid publicBiddingID);
    }
}
