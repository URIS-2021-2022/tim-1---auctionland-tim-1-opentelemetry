using DocumentMsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.ServiceCalls.CustomerMicroservice
{
    public interface ICustomerMicroservice
    {
        public bool GetCustomers(CustomerDto customerDto);
    }
}
