using AutoMapper;
using PublicBiddingRegistrationMicroservice.Entities;
using PublicBiddingRegistrationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentForApplication, PaymentDto>();
            CreateMap<PaymentForApplication, PaymentCreationDto>();
            CreateMap<PaymentForApplication, PaymentUpdateDto>();
        }
    }
}
