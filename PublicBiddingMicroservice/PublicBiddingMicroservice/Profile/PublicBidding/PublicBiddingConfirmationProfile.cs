using AutoMapper;
using PublicBiddingMicroservice.Entities;
using PublicBiddingMicroservice.Models;

namespace PublicBiddingMicroservice.Profiles
{
    public class PublicBiddingConfirmationProfile : Profile
    {
        public PublicBiddingConfirmationProfile()
        {
            CreateMap<PublicBiddingConfirmation, PublicBiddingConfirmationDto>();
            CreateMap<PublicBidding, PublicBiddingConfirmation>();
        }
    }
}

