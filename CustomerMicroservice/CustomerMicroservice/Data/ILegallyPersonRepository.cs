using CustomerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Data
{
    public interface ILegallyPersonRepository
    {
        List<LegallyPerson> GetLegallyPeople();
        LegallyPerson GetLegallyPersonById(Guid legallyPersonID);
        LegallyPersonConfirmation CreateLegallyPerson(LegallyPerson legallyPerson);
        void UpdateLegallyPerson(LegallyPerson legallyPerson);
        void DeleteLegallyPerson(Guid legallyPersonID);
        bool SaveChanges();
    }
}
