using DocumentMsProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Data
{
    public interface ILeaseAgreementRepository
    {
        LeaseAgreementConfirmation CreateLeaseAgreement(LeaseAgreement leaseAgreement);

        void UpdateLeaseAgreement(LeaseAgreement leaseAgreement);

        List<LeaseAgreement> GetAllLeases();

        LeaseAgreement GetLeaseById(Guid leaseID);

        void DeleteLease(Guid leaseID);

        bool SaveChanges();
    }
}
