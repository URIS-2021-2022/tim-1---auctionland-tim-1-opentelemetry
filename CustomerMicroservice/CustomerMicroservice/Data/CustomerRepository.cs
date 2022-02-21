using AutoMapper;
using CustomerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext context;
        private readonly IMapper mapper;

        public CustomerRepository(IMapper mapper, CustomerContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public CustomerConfirmation CreateCustomer(Customer customer)
        {
            var createdCustomer = context.Add(customer);
            return mapper.Map<CustomerConfirmation>(createdCustomer.Entity);
        }

        public void DeleteCustomer(Guid customerID)
        {
            var customer = GetCustomerById(customerID);
            context.Remove(customer);
        }

        public Customer GetCustomerById(Guid customerID)
        {
            return context.Customers.FirstOrDefault(e => e.CustomerID == customerID);
        }

        public List<Customer> GetCustomers()
        {
            return context.Customers.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateCustomer(Customer customer)
        {
            // 
        }
    }
}
