using AutoMapper;
using PublicBiddingMicroservice.Entities;
using PublicBiddingMicroservice.Models;

namespace PublicBiddingMicroservice.Profiles
{
    public class PublicBiddingTypeConfirmationProfile : Profile
    {
        public PublicBiddingTypeConfirmationProfile()
        {
            CreateMap<PublicBiddingTypeConfirmation, PublicBiddingTypeConfirmationDto>();
            CreateMap<PublicBiddingType, PublicBiddingTypeConfirmation>();
        }
    }
}

