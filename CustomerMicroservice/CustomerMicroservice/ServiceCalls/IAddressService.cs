﻿using CustomerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.ServiceCalls
{
    public interface IAddressService
    {
        public Task<AddressDto> GetAddress(Guid addressId);
    }
}
