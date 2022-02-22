using AutoMapper;
using CustomerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Profiles
{
    public class PublicBiddingProfile : Profile
    {
        public PublicBiddingProfile()
        {
            CreateMap<CustomerCreationDto, PublicBiddingDto>();
        }
    }
}
