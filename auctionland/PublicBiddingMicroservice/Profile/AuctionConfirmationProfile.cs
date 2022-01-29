using AutoMapper;
using PublicBiddingMicroservice.Entities;
using PublicBiddingMicroservice.Models;

namespace PublicBiddingMicroservice.Profiles
{
    public class AuctionConfirmationProfile : Profile
    {
        public AuctionConfirmationProfile()
        {
            CreateMap<AuctionConfirmation, AuctionConfirmationDto>();
            CreateMap<Auction, AuctionConfirmation>();
        }
    }
}

