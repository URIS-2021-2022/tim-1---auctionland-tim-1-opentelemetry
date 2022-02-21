using AutoMapper;
using PublicBiddingMicroservice.Entities;
using PublicBiddingMicroservice.Models;

namespace PublicBiddingMicroservice.Profiles
{
    public class StageProfile : Profile
    {
        public StageProfile()
        {
            CreateMap<Stage, StageDto>();
            CreateMap<StageCreationDto, Stage>();
            CreateMap<StageUpdateDto, Stage>();
            CreateMap<Stage, Stage>();
        }
    }
}

