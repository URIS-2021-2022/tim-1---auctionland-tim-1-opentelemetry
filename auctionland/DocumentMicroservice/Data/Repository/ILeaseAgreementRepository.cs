using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Data.Repository
{
    public interface ILeaseAgreementRepository
    {
        LeaseAgreementConfirmation CreateLease(LeaseAgreement leaseAgreement);

        void UpdateLease(LeaseAgreement leaseAgreement);

        List<LeaseAgreement> GetAllLeases();

        LeaseAgreement GetLeaseById(Guid leaseID);

        void DeleteLease(Guid leaseID);

        bool SaveChanges();
    }
}
