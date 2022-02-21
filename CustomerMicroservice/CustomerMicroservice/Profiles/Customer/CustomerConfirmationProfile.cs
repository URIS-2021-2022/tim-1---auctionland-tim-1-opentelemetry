using AutoMapper;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Profiles
{
    public class CustomerConfirmationProfile : Profile
    {
        public CustomerConfirmationProfile()
        {
            CreateMap<CustomerConfirmation, CustomerConfirmationDto>();
            CreateMap<Customer, CustomerConfirmation>();
        }
    }
}
