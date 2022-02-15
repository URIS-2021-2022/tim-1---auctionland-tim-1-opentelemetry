using AutoMapper;
using PublicBiddingRegistrationMicroservice.Entities;
using PublicBiddingRegistrationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBiddingRegistrationMicroservice.Profiles
{
    public class PaymentConfirmatioProfile : Profile
    {
        public PaymentConfirmatioProfile()
        {
            CreateMap<PaymentConfirmation, PaymentConfirmationDto>();
            CreateMap<PaymentForApplication, PaymentConfirmation>();
        }
    }
}
