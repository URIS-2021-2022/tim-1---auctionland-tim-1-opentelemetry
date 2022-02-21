using CustomerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Data
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
        Customer GetCustomerById(Guid customerID);
        CustomerConfirmation CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Guid customerID);
        bool SaveChanges();
    }
}
