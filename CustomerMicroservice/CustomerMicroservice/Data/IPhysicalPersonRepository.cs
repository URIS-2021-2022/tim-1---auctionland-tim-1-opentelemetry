using CustomerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Data
{
    public interface IPhysicalPersonRepository
    {
        List<PhysicalPerson> GetPhysicalPeople();
        PhysicalPerson GetPhysicalPersonById(Guid physicalPersonID);
        PhysicalPersonConfirmation CreatePhysicalPerson(PhysicalPerson physicalPerson);
        void UpdatePhysicalPerson(PhysicalPerson physicalPerson);
        void DeletePhysicalPerson(Guid physicalPersonID);
        bool SaveChanges();
    }
}
