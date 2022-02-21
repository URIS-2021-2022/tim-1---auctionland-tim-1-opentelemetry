using AutoMapper;
using PublicBiddingMicroservice.Entities;
using PublicBiddingMicroservice.Models;

namespace PublicBiddingMicroservice.Profiles
{
    public class AuctionProfile : Profile
    {
        public AuctionProfile()
        {
            CreateMap<Auction, AuctionDto>();
            CreateMap<AuctionCreationDto, Auction>();
            CreateMap<AuctionUpdateDto, Auction>();
            CreateMap<Auction, Auction>();
        }
    }
}

