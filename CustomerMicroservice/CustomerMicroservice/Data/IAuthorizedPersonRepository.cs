using CustomerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Data
{
    public interface IAuthorizedPersonRepository
    {
        List<AuthorizedPerson> GetAuthorizedPeople();
        AuthorizedPerson GetAuthorizedPersonById(Guid authorizedPersonID);
        AuthorizedPersonConfirmation CreateAuthorizedPerson(AuthorizedPerson authorizedPerson);
        void UpdateAuthorizedPerson(AuthorizedPerson authorizedPerson);
        void DeleteAuthorizedPerson(Guid authorizedPersonID);
        bool SaveChanges();
    }
}
