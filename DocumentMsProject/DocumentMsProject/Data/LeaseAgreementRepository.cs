using AutoMapper;
using DocumentMsProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Data
{
    public class LeaseAgreementRepository : ILeaseAgreementRepository
    {
        private readonly IMapper mapper;
        private readonly DocumentMsContext context;

        public LeaseAgreementRepository(IMapper mapper, DocumentMsContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public LeaseAgreementConfirmation CreateLeaseAgreement(LeaseAgreement leaseAgreement)
        {
            var created = context.Add(leaseAgreement);
            return mapper.Map<LeaseAgreementConfirmation>(created.Entity);
        }

        public void DeleteLease(Guid leaseID)
        {
            context.LeaseAgreement.Remove(GetLeaseById(leaseID));
        }

        public List<LeaseAgreement> GetAllLeases()
        {
            return context.LeaseAgreement.ToList();
        }

        public LeaseAgreement GetLeaseById(Guid leaseID)
        {
            return context.LeaseAgreement.FirstOrDefault(e => e.LeaseAgreementID.Equals(leaseID));
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateLeaseAgreement(LeaseAgreement leaseAgreement)
        {
            //throw new NotImplementedException();
        }
    }
}
