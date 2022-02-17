using PublicBiddingMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicBiddingMicroservice.Data
{
    public class AuctionMockRepository : IAuctionRepository
    {
        public static List<Auction> Auctions { get; set; } = new List<Auction>();

        public AuctionMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            Auctions.AddRange(new List<Auction>
            {
                new Auction
                {
                    AuctionId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Number   = 1,
                    Year   = 2021,
                    Date  = DateTime.Parse("2020-11-15T09:00:00"),
                    PriceIncrease   = 10.00,
                    ApplicationDeadline  = DateTime.Parse("2020-11-15T09:00:00"),
                },
                new Auction
                {
                    AuctionId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Number   = 1,
                    Year   = 2021,
                    Date  = DateTime.Parse("2020-11-15T09:00:00"),
                    PriceIncrease   = 10.00,
                    ApplicationDeadline  = DateTime.Parse("2020-11-15T09:00:00"),
                }
            });
        }

        public List<Auction> GetAuctions()
        {
            return (from e in Auctions
                    select e).ToList();
        }

        public Auction GetAuctionById(Guid auctionId)
        {
            return Auctions.FirstOrDefault(e => e.AuctionId == auctionId);
        }

        public AuctionConfirmation CreateAuction(Auction auction)
        {
            auction.AuctionId = Guid.NewGuid();
            Auctions.Add(auction);
            var exam = GetAuctionById(auction.AuctionId);

            return new AuctionConfirmation
            {
                AuctionId = exam.AuctionId,
                Date = exam.Date
            };
        }

        public void DeleteAuction(Guid auctionId)
        {
            Auctions.Remove(Auctions.FirstOrDefault(e => e.AuctionId == auctionId));
        }

        public void UpdateAuction(Auction auction)
        {
            var exam = GetAuctionById(auction.AuctionId);

            exam.AuctionId = auction.AuctionId;
            exam.Date = auction.Date;
        }

        //Koristi se za Update kako u kontroleru ne bi morali da menjamo logiku pri zameni repozitorijuma
        public bool SaveChanges()
        {            
            return true;
        }
    }
}
