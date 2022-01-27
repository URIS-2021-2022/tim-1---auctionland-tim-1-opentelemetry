using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Data.Repository
{
    public class LeaseAgreementRepositrory : ILeaseAgreementRepository
    {
        private readonly DocumentContext agreementContext;
        public LeaseAgreement CreateLease(LeaseAgreement leaseAgreement)
        {
            leaseAgreement = new LeaseAgreement();

            leaseAgreement.LeaseAgreementID = Guid.NewGuid();


            agreementContext.LeaseAgreements.Add(leaseAgreement);
            return leaseAgreement;
        }

        public void DeleteLease(Guid leaseID)
        {
            var lease = GetLeaseById(leaseID);
            agreementContext.LeaseAgreements.Remove(lease);
        }

        public List<LeaseAgreement> GetAllLeases()
        {
            return agreementContext.LeaseAgreements.ToList();
        }

        public LeaseAgreement GetLeaseById(Guid leaseID)
        {
            return agreementContext.LeaseAgreements.FirstOrDefault(e => e.LeaseAgreementID == leaseID);
        }

        public LeaseAgreement UpdateLease(LeaseAgreement leaseAgreement)
        {
            return leaseAgreement;
        }
    }
}
