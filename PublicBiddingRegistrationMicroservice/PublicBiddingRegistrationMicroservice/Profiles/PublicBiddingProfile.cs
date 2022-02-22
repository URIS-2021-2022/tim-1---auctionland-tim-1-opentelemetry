using AutoMapper;
using PublicBiddingRegistrationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Profiles
{
    public class PublicBiddingProfile : Profile
    {
        public PublicBiddingProfile()
        {
            CreateMap<PaymentCreationDto, PublicBiddingDto>().ForMember(dest => dest.PublicBiddingId, opt => opt.MapFrom(src => $"{src.PublicBiddingId}"));
        }
    }
}
