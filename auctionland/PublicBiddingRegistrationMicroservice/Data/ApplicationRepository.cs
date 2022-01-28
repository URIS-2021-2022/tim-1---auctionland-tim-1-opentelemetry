using AutoMapper;
using PublicBiddingRegistrationMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Data
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IMapper mapper;
        private readonly ApplicationContext context;

        public ApplicationRepository(IMapper mapper, ApplicationContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public ApplicationConfirmation CreateApplication(ApplicationForPublicBidding applicationForPublicBidding)
        {
            var createdEntity = context.ApplicationForPublicBidding.Add(applicationForPublicBidding);
            return mapper.Map<ApplicationConfirmation>(createdEntity.Entity);
        }

        public void DeleteAplication(Guid applicationId)
        {
            context.ApplicationForPublicBidding.Remove(GetApplicationById(applicationId));
        }

        public ApplicationForPublicBidding GetApplicationById(Guid applicationId)
        {
            return context.ApplicationForPublicBidding.FirstOrDefault(e => e.ApplicationId.Equals(applicationId));
        }

        public List<ApplicationForPublicBidding> GetApplications()
        {
            return context.ApplicationForPublicBidding.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateApplication(ApplicationForPublicBidding applicationForPublicBidding)
        {
            //nema implementaciju
        }
    }
}
