using AutoMapper;
using CustomerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Data
{
    public class LegallyPersonRepository : ILegallyPersonRepository
    {
        private readonly CustomerContext context;
        private readonly IMapper mapper;

        public LegallyPersonRepository(IMapper mapper, CustomerContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public LegallyPersonConfirmation CreateLegallyPerson(LegallyPerson legallyPerson)
        {
            var createdLegallyPerson = context.Add(legallyPerson);
            return mapper.Map<LegallyPersonConfirmation>(createdLegallyPerson.Entity);
        }

        public void DeleteLegallyPerson(Guid legallyPersonID)
        {
            var legallyPerson = GetLegallyPersonById(legallyPersonID);
            context.Remove(legallyPerson);
        }

        public List<LegallyPerson> GetLegallyPeople()
        {
            return context.LegallyPeople.ToList();
        }

        public LegallyPerson GetLegallyPersonById(Guid legallyPersonID)
        {
            return context.LegallyPeople.FirstOrDefault(e => e.LegallyPersonID == legallyPersonID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateLegallyPerson(LegallyPerson legallyPerson)
        {
            throw new NotImplementedException();
        }
    }
}
