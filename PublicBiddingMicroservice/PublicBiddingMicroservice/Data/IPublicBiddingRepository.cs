using PublicBiddingMicroservice.Entities;
using System;
using System.Collections.Generic;

namespace PublicBiddingMicroservice.Data
{
    public interface IPublicBiddingRepository
    {
        List<PublicBidding> GetPublicBiddings(int numberOfParticipants = 0);

        PublicBidding GetPublicBiddingById(Guid publicBiddingId);

        PublicBiddingConfirmation CreatePublicBidding(PublicBidding publicBidding);

        void UpdatePublicBidding(PublicBidding publicBidding);

        void DeletePublicBidding(Guid publicBiddingId);

        bool SaveChanges();
    }
}
