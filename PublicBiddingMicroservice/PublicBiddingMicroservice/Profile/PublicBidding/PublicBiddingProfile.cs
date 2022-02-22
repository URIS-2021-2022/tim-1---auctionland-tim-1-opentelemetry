using AutoMapper;
using PublicBiddingMicroservice.Entities;
using PublicBiddingMicroservice.Models;

namespace PublicBiddingMicroservice.Profiles
{
    public class PubblicBiddingProfile : Profile
    {
        public PubblicBiddingProfile()
        {
            CreateMap<PublicBidding, PublicBiddingDto>();
            CreateMap<PublicBiddingCreationDto, PublicBidding>();
            CreateMap<PublicBiddingUpdateDto, PublicBidding>();
            CreateMap<PublicBidding, PublicBidding>();
        }
    }
}

