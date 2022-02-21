using AutoMapper;
using PublicBiddingMicroservice.Entities;
using PublicBiddingMicroservice.Models;

namespace PublicBiddingMicroservice.Profiles
{
    public class StatusConfirmationProfile : Profile
    {
        public StatusConfirmationProfile()
        {
            CreateMap<StatusConfirmation, StatusConfirmationDto>();
            CreateMap<Status, StatusConfirmation>();
        }
    }
}

