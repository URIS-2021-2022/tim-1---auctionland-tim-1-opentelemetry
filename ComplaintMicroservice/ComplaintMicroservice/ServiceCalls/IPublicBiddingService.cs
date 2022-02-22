using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplaintMicroservice.Models;

namespace ComplaintMicroservice.ServiceCalls
{
    public interface IPublicBiddingService
    {
        public Task<PublicBiddingDto> GetPublicBiddingById(Guid publicBiddingId);
    }
}
