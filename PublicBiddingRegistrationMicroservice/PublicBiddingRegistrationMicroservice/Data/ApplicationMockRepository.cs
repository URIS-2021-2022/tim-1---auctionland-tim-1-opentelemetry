using PublicBiddingRegistrationMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Data
{
    public class ApplicationMockRepository : IApplicationRepository
    {
        public static List<ApplicationForPublicBidding> ApplicationForPublics { get; set; } = new List<ApplicationForPublicBidding>();

        public ApplicationMockRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            ApplicationForPublics.AddRange(new List<ApplicationForPublicBidding>
            {
                new ApplicationForPublicBidding
                {
                    ApplicationId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    PaymentId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    BuyerId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f")
                },
                new ApplicationForPublicBidding
                {
                    ApplicationId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    PaymentId = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    BuyerId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3")
                }
            });
        }

        public ApplicationConfirmation CreateApplication(ApplicationForPublicBidding applicationForPublicBidding)
        {
            applicationForPublicBidding.ApplicationId = Guid.NewGuid();
            ApplicationForPublics.Add(applicationForPublicBidding);
            ApplicationForPublicBidding application = GetApplicationById(applicationForPublicBidding.ApplicationId);

            return new ApplicationConfirmation
            {
                ApplicationId = application.ApplicationId,
                PaymentId = application.PaymentId,
                BuyerId = application.BuyerId
            };
        }

        public void DeleteAplication(Guid applicationId)
        {
            ApplicationForPublics.Remove(ApplicationForPublics.FirstOrDefault(e => e.ApplicationId == applicationId));
        }

        public ApplicationForPublicBidding GetApplicationById(Guid applicationId)
        {
            return ApplicationForPublics.FirstOrDefault(e => e.ApplicationId == applicationId);
        }

        public List<ApplicationForPublicBidding> GetApplications()
        {
            return (from e in ApplicationForPublics
                    select e).ToList();
        }

        public void UpdateApplication(ApplicationForPublicBidding applicationForPublicBidding)
        {
            var application = GetApplicationById(applicationForPublicBidding.ApplicationId);

            application.PaymentId = applicationForPublicBidding.PaymentId;
            application.BuyerId = applicationForPublicBidding.BuyerId;
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
