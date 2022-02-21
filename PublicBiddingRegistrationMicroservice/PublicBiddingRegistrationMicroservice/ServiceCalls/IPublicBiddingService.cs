﻿using PublicBiddingRegistrationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.ServiceCalls
{
    public interface IPublicBiddingService
    {
        public bool GetPublicBidding(PublicBiddingDto publicBiddingDto);
    }
}