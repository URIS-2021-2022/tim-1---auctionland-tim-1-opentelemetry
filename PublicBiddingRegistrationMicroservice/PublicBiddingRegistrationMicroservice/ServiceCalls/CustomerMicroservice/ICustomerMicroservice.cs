using PublicBiddingRegistrationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.ServiceCalls.CustomerMicroservice
{
    public interface ICustomerMicroservice
    {
        Task<CustomerDto> GetCustomerByIdAsync(Guid? customerId);
    }
}
