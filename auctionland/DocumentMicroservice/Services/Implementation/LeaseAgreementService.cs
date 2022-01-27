using DocumentMicroservice.Data.Repository;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Services.Implementation
{
    public class LeaseAgreementService : ILeaseOfAgreement
    {
        private readonly ILeaseAgreementRepository agreementRepository;
        public ResponseLeaseAgreementDto CreateLease(RequestLeaseAgreementDto leaseDto)
        {
            LeaseAgreement leaseAgreement = new LeaseAgreement() {
                LeaseTypeOfGuarantee = leaseDto.LeaseTypeOfGuarantee,
                LeaseAgreementMaturities = leaseDto.LeaseAgreementMaturities,
                LeaseAgreementEntryDate = leaseDto.LeaseAgreementEntryDate,
                MinisterID = leaseDto.MinisterID,
                PublicBiddingID = leaseDto.PublicBiddingID,
                PersonID = leaseDto.PersonID,
                DeadlineForLandRestitution = leaseDto.DeadlineForLandRestitution,
                PlaceOfSigning = leaseDto.PlaceOfSigning,
                DateOfSigning = leaseDto.DateOfSigning
            };

            agreementRepository.CreateLease(leaseAgreement);

            ResponseLeaseAgreementDto response = new ResponseLeaseAgreementDto()
            {
                LeaseTypeOfGuarantee = leaseAgreement.LeaseTypeOfGuarantee,
                LeaseAgreementMaturities = leaseAgreement.LeaseAgreementMaturities,
                LeaseAgreementEntryDate = leaseAgreement.LeaseAgreementEntryDate,
                MinisterID = leaseAgreement.MinisterID,
                PublicBiddingID = leaseAgreement.PublicBiddingID,
                PersonID = leaseAgreement.PersonID,
                DeadlineForLandRestitution = leaseAgreement.DeadlineForLandRestitution,
                PlaceOfSigning = leaseAgreement.PlaceOfSigning,
                DateOfSigning = leaseAgreement.DateOfSigning
            };

            return response;
        }

        public void DeleteLease(Guid leaseID)
        {
            agreementRepository.DeleteLease(leaseID);
        }

        public List<ResponseLeaseAgreementDto> GetAllLeases()
        {
            List<LeaseAgreement> leases = agreementRepository.GetAllLeases();
            List<ResponseLeaseAgreementDto> responseLeasesDtos = new List<ResponseLeaseAgreementDto>();

            foreach (LeaseAgreement lease in leases)
            {
                ResponseLeaseAgreementDto responseDto = new ResponseLeaseAgreementDto()
                {
                    LeaseTypeOfGuarantee = lease.LeaseTypeOfGuarantee,
                    LeaseAgreementMaturities = lease.LeaseAgreementMaturities,
                    LeaseAgreementEntryDate = lease.LeaseAgreementEntryDate,
                    MinisterID = lease.MinisterID,
                    PublicBiddingID = lease.PublicBiddingID,
                    PersonID = lease.PersonID,
                    DeadlineForLandRestitution = lease.DeadlineForLandRestitution,
                    PlaceOfSigning = lease.PlaceOfSigning,
                    DateOfSigning = lease.DateOfSigning
                };
                responseLeasesDtos.Add(responseDto);
            }


            return responseLeasesDtos;
        }

        public ResponseLeaseAgreementDto GetLeaseById(Guid leaseID)
        {
            LeaseAgreement lease = agreementRepository.GetLeaseById(leaseID);

            ResponseLeaseAgreementDto response = new ResponseLeaseAgreementDto()
            {
                LeaseTypeOfGuarantee = lease.LeaseTypeOfGuarantee,
                LeaseAgreementMaturities = lease.LeaseAgreementMaturities,
                LeaseAgreementEntryDate = lease.LeaseAgreementEntryDate,
                MinisterID = lease.MinisterID,
                PublicBiddingID = lease.PublicBiddingID,
                PersonID = lease.PersonID,
                DeadlineForLandRestitution = lease.DeadlineForLandRestitution,
                PlaceOfSigning = lease.PlaceOfSigning,
                DateOfSigning = lease.DateOfSigning
            };

            return response;
        }

        public ResponseLeaseAgreementDto UpdateLease(RequestLeaseAgreementDto leaseDto)
        {
            LeaseAgreement leaseAgreement = new LeaseAgreement()
            {
                LeaseTypeOfGuarantee = leaseDto.LeaseTypeOfGuarantee,
                LeaseAgreementMaturities = leaseDto.LeaseAgreementMaturities,
                LeaseAgreementEntryDate = leaseDto.LeaseAgreementEntryDate,
                MinisterID = leaseDto.MinisterID,
                PublicBiddingID = leaseDto.PublicBiddingID,
                PersonID = leaseDto.PersonID,
                DeadlineForLandRestitution = leaseDto.DeadlineForLandRestitution,
                PlaceOfSigning = leaseDto.PlaceOfSigning,
                DateOfSigning = leaseDto.DateOfSigning
            };

            agreementRepository.UpdateLease(leaseAgreement);

            ResponseLeaseAgreementDto response = new ResponseLeaseAgreementDto()
            {
                LeaseTypeOfGuarantee = leaseAgreement.LeaseTypeOfGuarantee,
                LeaseAgreementMaturities = leaseAgreement.LeaseAgreementMaturities,
                LeaseAgreementEntryDate = leaseAgreement.LeaseAgreementEntryDate,
                MinisterID = leaseAgreement.MinisterID,
                PublicBiddingID = leaseAgreement.PublicBiddingID,
                PersonID = leaseAgreement.PersonID,
                DeadlineForLandRestitution = leaseAgreement.DeadlineForLandRestitution,
                PlaceOfSigning = leaseAgreement.PlaceOfSigning,
                DateOfSigning = leaseAgreement.DateOfSigning
            };

            return response;
        }
    }
}
