using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Services
{
    public interface ILeaseOfAgreement
    {
        ResponseLeaseAgreementDto CreateLease(RequestLeaseAgreementDto leaseDto);

        ResponseLeaseAgreementDto UpdateLease(RequestLeaseAgreementDto leaseDto);

        List<ResponseLeaseAgreementDto> GetAllLeases();

        ResponseLeaseAgreementDto GetLeaseById(Guid leaseID);

        void DeleteLease(Guid leaseID);
    }
}
