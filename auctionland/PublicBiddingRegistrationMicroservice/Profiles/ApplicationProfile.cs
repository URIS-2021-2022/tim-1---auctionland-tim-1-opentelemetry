using AutoMapper;
using PublicBiddingRegistrationMicroservice.Entities;
using PublicBiddingRegistrationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ApplicationForPublicBidding, ApplicationDto>();
            CreateMap<ApplicationForPublicBidding, ApplicationCreationDto>();
            CreateMap<ApplicationForPublicBidding, ApplicationUpdateDto>();
        }
    }
}
