using AdMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdMicroservice.ServiceCalls
{
    public interface IPublicBiddingService
    {
        public Task<PublicBiddingDto> GetPublicBiddingById(Guid guid);

    }
}
