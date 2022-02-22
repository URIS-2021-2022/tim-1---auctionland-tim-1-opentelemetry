using AutoMapper;
using DocumentMsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Profiles
{
    public class PublicBiddingProfile : Profile
    {
        public PublicBiddingProfile()
        {
            CreateMap<PublicBiddingDto, LeaseAgreementCreationDto>().ForMember(dest => dest.PublicBiddingID, opt => opt.MapFrom(src => $"{src.PublicBiddingId}"));
        }
    }
}
