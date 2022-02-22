using AutoMapper;
using CustomerMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Data
{
    public class PhysicalPersonRepository : IPhysicalPersonRepository
    {
        private readonly CustomerContext context;
        private readonly IMapper mapper;

        public PhysicalPersonRepository(IMapper mapper, CustomerContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public PhysicalPersonConfirmation CreatePhysicalPerson(PhysicalPerson physicalPerson)
        {
            var createdPhysicalPerson = context.Add(physicalPerson);
            return mapper.Map<PhysicalPersonConfirmation>(createdPhysicalPerson.Entity);
        }

        public void DeletePhysicalPerson(Guid physicalPersonID)
        {
            var physicalPerson = GetPhysicalPersonById(physicalPersonID);
            context.Remove(physicalPerson);
        }

        public List<PhysicalPerson> GetPhysicalPeople()
        {
            return context.PhysicalPerson.ToList();
        }

        public PhysicalPerson GetPhysicalPersonById(Guid physicalPersonID)
        {
            return context.PhysicalPerson.FirstOrDefault(e => e.CustomerID == physicalPersonID);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdatePhysicalPerson(PhysicalPerson physicalPerson)
        {
            throw new NotImplementedException();
        }
    }
}
