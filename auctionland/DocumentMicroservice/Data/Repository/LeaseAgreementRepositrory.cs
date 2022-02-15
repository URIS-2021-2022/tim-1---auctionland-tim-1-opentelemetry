using AutoMapper;
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
        private readonly IMapper mapper;

        public LeaseAgreementRepositrory(DocumentContext agreementContext, IMapper mapper)
        {
            this.agreementContext = agreementContext;
            this.mapper = mapper;
        }

        public LeaseAgreementConfirmation CreateLease(LeaseAgreement leaseAgreement)
        {
            /*leaseAgreement = new LeaseAgreement();

            leaseAgreement.LeaseAgreementID = Guid.NewGuid();


            agreementContext.LeaseAgreements.Add(leaseAgreement);
            return leaseAgreement;*/
            var createdEntity = agreementContext.Add(leaseAgreement);
            return mapper.Map<LeaseAgreementConfirmation>(createdEntity.Entity);
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

        public bool SaveChanges()
        {
            return agreementContext.SaveChanges() > 0;
        }

        public void UpdateLease(LeaseAgreement leaseAgreement)
        {
            //return leaseAgreement;
        }
    }
}
