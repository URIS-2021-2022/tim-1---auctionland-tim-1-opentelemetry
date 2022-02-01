using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PublicBiddingMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PublicBiddingMicroservice.Data
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly PublicBiddingContext context;
        private readonly IMapper mapper;

        public AuctionRepository(PublicBiddingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<Auction> GetAuctions()
        {
            return context.Auction.ToList();
        }

        public Auction GetAuctionById(Guid auctionId)
        {
            return context.Auction.FirstOrDefault (e => e.AuctionId == auctionId);
        }

        public AuctionConfirmation CreateAuction(Auction auction)
        {
            var createdEntity = context.Add(auction);
            return mapper.Map<AuctionConfirmation>(createdEntity.Entity);
        }                
        
        public void UpdateAuction(Auction auction)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteAuction(Guid auctionId)
        {
            context.Auction.Remove(GetAuctionById(auctionId));
        }
    }
}
