﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.ServiceCalls
{
    public interface IAddressService
    {
        public bool GetAddressById(Guid addressId);
    }
}
