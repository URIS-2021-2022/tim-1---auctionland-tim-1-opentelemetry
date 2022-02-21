using AutoMapper;
using CustomerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Data
{
    public class AuthorizedPersonCustomerRepository : IAuthorizedPersonCustomerRepository
    {
        private readonly CustomerContext context;
        private readonly IMapper mapper;

        public AuthorizedPersonCustomerRepository(IMapper mapper, CustomerContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public AuthorizedPersonCustomerConfirmation CreateAuthorizedPersonCustomer(AuthorizedPersonCustomer authorizedPersonCustomer)
        {
            var createdAuthorizedPersonCustomer = context.Add(authorizedPersonCustomer);
            return mapper.Map<AuthorizedPersonCustomerConfirmation>(createdAuthorizedPersonCustomer.Entity);
        }

        public void DeleteAuthorizedPersonCustomer(Guid authorizedPersonCustomerID)
        {
            var authorizedPersonCustomer = GetAuthorizedPersonCustomerById(authorizedPersonCustomerID);
            context.Remove(authorizedPersonCustomer);
        }

        public AuthorizedPersonCustomer GetAuthorizedPersonCustomerById(Guid authorizedPersonCustomerID)
        {
            return context.AuthorizedPersonCustomers.FirstOrDefault(e => e.AuthorizedPersonCustomerID == authorizedPersonCustomerID);
        }

        public List<AuthorizedPersonCustomer> GetAuthorizedPersonCustomers()
        {
            return context.AuthorizedPersonCustomers.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateAuthorizedPersonCustomer(AuthorizedPersonCustomer authorizedPersonCustomer)
        {
            throw new NotImplementedException();
        }
    }
}
