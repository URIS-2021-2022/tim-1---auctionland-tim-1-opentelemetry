using AutoMapper;
using PublicBiddingMicroservice.Entities;
using PublicBiddingMicroservice.Models;

namespace PublicBiddingMicroservice.Profiles
{
    public class StageConfirmationProfile : Profile
    {
        public StageConfirmationProfile()
        {
            CreateMap<StageConfirmation, StageConfirmationDto>();
            CreateMap<Stage, StageConfirmation>();
        }
    }
}

