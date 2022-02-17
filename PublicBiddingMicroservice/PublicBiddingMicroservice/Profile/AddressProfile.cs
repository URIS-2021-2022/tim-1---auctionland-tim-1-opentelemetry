using AutoMapper;
using PublicBiddingMicroservice.Entities;
using PublicBiddingMicroservice.Models;

namespace PublicBiddingMicroservice.Profiles
{
    public class PublicBiddingProfile : Profile
    {
        public PublicBiddingProfile()
        {
            CreateMap<PublicBidding, PublicBiddingDto>();
            CreateMap<PublicBiddingCreationDto, PublicBidding>();
            CreateMap<PublicBiddingUpdateDto, PublicBidding>();
            CreateMap<PublicBidding, PublicBidding>();
        }
    }
}

