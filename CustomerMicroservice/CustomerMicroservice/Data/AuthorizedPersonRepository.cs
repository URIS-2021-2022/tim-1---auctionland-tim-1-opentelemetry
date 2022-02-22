using AutoMapper;
using CustomerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Data
{
    public class AuthorizedPersonRepository : IAuthorizedPersonRepository
    {
        private readonly CustomerContext context;
        private readonly IMapper mapper;

        public AuthorizedPersonRepository(IMapper mapper, CustomerContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public AuthorizedPersonConfirmation CreateAuthorizedPerson(AuthorizedPerson authorizedPerson)
        {
            var createdAuthorizedPerson = context.Add(authorizedPerson);
            return mapper.Map<AuthorizedPersonConfirmation>(createdAuthorizedPerson.Entity);
        }

        public void DeleteAuthorizedPerson(Guid authorizedPersonID)
        {
            var authorizedPerson = GetAuthorizedPersonById(authorizedPersonID);
            context.Remove(authorizedPerson);
        }

        public List<AuthorizedPerson> GetAuthorizedPeople()
        {
            return context.AuthorizedPerson.ToList();
        }

        public AuthorizedPerson GetAuthorizedPersonById(Guid authorizedPersonID)
        {
            return context.AuthorizedPerson.FirstOrDefault(e => e.AuthorizedPersonID == authorizedPersonID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateAuthorizedPerson(AuthorizedPerson authorizedPerson)
        {
            throw new NotImplementedException();
        }
    }
}
