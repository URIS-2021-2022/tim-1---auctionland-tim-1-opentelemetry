using CustomerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Data
{
    public interface IAuthorizedPersonCustomerRepository
    {
        List<AuthorizedPersonCustomer> GetAuthorizedPersonCustomers();
        AuthorizedPersonCustomer GetAuthorizedPersonCustomerById(Guid authorizedPersonCustomerID);
        AuthorizedPersonCustomerConfirmation CreateAuthorizedPersonCustomer(AuthorizedPersonCustomer authorizedPersonCustomer);
        void UpdateAuthorizedPersonCustomer(AuthorizedPersonCustomer authorizedPersonCustomer);
        void DeleteAuthorizedPersonCustomer(Guid authorizedPersonCustomerID);
        bool SaveChanges();
    }
}
