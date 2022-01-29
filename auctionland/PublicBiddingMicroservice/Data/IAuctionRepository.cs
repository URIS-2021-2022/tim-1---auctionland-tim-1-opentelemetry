using PublicBiddingMicroservice.Entities;
using System;
using System.Collections.Generic;

namespace PublicBiddingMicroservice.Data
{
    public interface IAuctionRepository
    {
        List<Auction> GetAuctions();

        Auction GetAuctionById(Guid auctionId);

        AuctionConfirmation CreateAuction(Auction auction);

        void UpdateAuction(Auction auction);

        void DeleteAuction(Guid auctionId);

        bool SaveChanges();
    }
}
