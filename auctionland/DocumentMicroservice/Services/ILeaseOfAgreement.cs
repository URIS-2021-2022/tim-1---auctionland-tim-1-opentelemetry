using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Services
{
    public interface ILeaseOfAgreement
    {
        LeaseAgreementUpdateDto CreateLease(LeaseAgreementDto leaseDto);

        LeaseAgreementUpdateDto UpdateLease(LeaseAgreementDto leaseDto);

        List<LeaseAgreementUpdateDto> GetAllLeases();

        LeaseAgreementUpdateDto GetLeaseById(Guid leaseID);

        void DeleteLease(Guid leaseID);
    }
}
