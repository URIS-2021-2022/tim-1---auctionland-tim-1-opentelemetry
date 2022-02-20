using PublicBiddingRegistrationMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Data
{
    public interface IApplicationRepository
    {
        List<ApplicationForPublicBidding> GetApplications();

        ApplicationForPublicBidding GetApplicationById(Guid applicationId);

        ApplicationConfirmation CreateApplication(ApplicationForPublicBidding applicationForPublicBidding);

        void UpdateApplication(ApplicationForPublicBidding applicationForPublicBidding);

        void DeleteAplication(Guid applicationId);

        bool SaveChanges();
    }
}
