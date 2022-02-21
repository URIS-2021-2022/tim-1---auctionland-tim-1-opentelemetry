using PublicBiddingMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingMicroservice.ServiceCalls
{
    public interface IAddressService
    {
        public bool GetAddressOfPubblicBidding(AddressDto addressDto);
    }
}
