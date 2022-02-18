using AutoMapper;
using PublicBiddingMicroservice.Entities;
using PublicBiddingMicroservice.Models;

namespace PublicBiddingMicroservice.Profiles
{
    public class PublicBiddingTypeProfile : Profile
    {
        public PublicBiddingTypeProfile()
        {
            CreateMap<PublicBiddingType, PublicBiddingTypeDto>();
            CreateMap<PublicBiddingTypeCreationDto, PublicBiddingType>();
            CreateMap<PublicBiddingTypeUpdateDto, PublicBiddingType>();
            CreateMap<PublicBiddingType, PublicBiddingType>();
        }
    }
}

