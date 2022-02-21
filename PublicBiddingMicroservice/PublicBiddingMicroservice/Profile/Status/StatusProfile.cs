using AutoMapper;
using PublicBiddingMicroservice.Entities;
using PublicBiddingMicroservice.Models;

namespace PublicBiddingMicroservice.Profiles
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<Status, StatusDto>();
            CreateMap<StatusCreationDto, Status>();
            CreateMap<StatusUpdateDto, Status>();
            CreateMap<Status, Status>();
        }
    }
}

