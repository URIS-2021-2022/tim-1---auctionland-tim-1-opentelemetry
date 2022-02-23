using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplaintMicroservice.Models;

namespace ComplaintMicroservice.ServiceCalls
{
    public interface ICustomerService
    {
        public Task<CustomerDto> GetCustomerById(Guid customerId);
    }
}
