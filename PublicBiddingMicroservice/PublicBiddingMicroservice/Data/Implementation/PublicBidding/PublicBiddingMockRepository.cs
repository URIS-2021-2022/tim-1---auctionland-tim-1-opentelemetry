using PublicBiddingMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicBiddingMicroservice.Data
{
    public class PublicBiddingMockRepository : IPublicBiddingRepository
    {
        public static List<PublicBidding> PublicBiddings { get; set; } = new List<PublicBidding>();

        public PublicBiddingMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            PublicBiddings.AddRange(new List<PublicBidding>
            {
                new PublicBidding
                {
                    PublicBiddingId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Date = DateTime.Parse("2020-11-15T09:00:00"),
                    StartTime  = DateTime.Parse("2020-11-15T09:00:00"),
                    EndTime  = DateTime.Parse("2020-11-15T12:00:00"),
                    StartingPricePerHe  = 10.00,
                    Exclude  = true,
                    AuctionedPrice  = 139.00,
                    LeasePeriod  = 1,
                    NumberOfParticipants  = 12,
                    DepositReplenishment  = 13.00,
                    Circle   = 1,
                    Status   ="Status1",
                },
                new PublicBidding
                {
                    PublicBiddingId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Date = DateTime.Parse("2020-11-15T09:00:00"),
                    StartTime  = DateTime.Parse("2020-11-15T09:00:00"),
                    EndTime  = DateTime.Parse("2020-11-15T12:00:00"),
                    StartingPricePerHe  = 10,
                    Exclude  = true,
                    AuctionedPrice  = 139,
                    LeasePeriod  = 1,
                    NumberOfParticipants  = 12,
                    DepositReplenishment  = 13,
                    Circle   = 1,
                    Status   ="Status1",
                }
            });
        }

        public List<PublicBidding> GetPublicBiddings(int numberOfParticipants = 0)
        {
            return (from e in PublicBiddings
                    where numberOfParticipants == 0 || e.NumberOfParticipants == numberOfParticipants
                    select e).ToList();
        }

        public PublicBidding GetPublicBiddingById(Guid publicBiddingId)
        {
            return PublicBiddings.FirstOrDefault(e => e.PublicBiddingId == publicBiddingId);
        }

        public PublicBiddingConfirmation CreatePublicBidding(PublicBidding publicBidding)
        {
            publicBidding.PublicBiddingId = Guid.NewGuid();
            PublicBiddings.Add(publicBidding);
            var exam = GetPublicBiddingById(publicBidding.PublicBiddingId);

            return new PublicBiddingConfirmation
            {
                PublicBiddingId = exam.PublicBiddingId,
                Date = exam.Date
            };
        }

        public void DeletePublicBidding(Guid publicBiddingId)
        {
            PublicBiddings.Remove(PublicBiddings.FirstOrDefault(e => e.PublicBiddingId == publicBiddingId));
        }

        public void UpdatePublicBidding(PublicBidding publicBidding)
        {
            var exam = GetPublicBiddingById(publicBidding.PublicBiddingId);

            exam.PublicBiddingId = publicBidding.PublicBiddingId;
            exam.Date = publicBidding.Date;
        }

        //Koristi se za Update kako u kontroleru ne bi morali da menjamo logiku pri zameni repozitorijuma
        public bool SaveChanges()
        {
            return true;
        }
    }
}
