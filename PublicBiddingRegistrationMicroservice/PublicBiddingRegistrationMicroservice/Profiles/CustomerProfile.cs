using AutoMapper;
using PublicBiddingRegistrationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<ApplicationDto, CustomerDto>().ForMember(dest => dest.CustomerID, opt => opt.MapFrom(src => $"{src.BuyerId}"));
        }
    }
}
