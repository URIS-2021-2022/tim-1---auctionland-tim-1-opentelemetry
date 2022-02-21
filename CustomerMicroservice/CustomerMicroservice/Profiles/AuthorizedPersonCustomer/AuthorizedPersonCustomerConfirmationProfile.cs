using AutoMapper;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models.AuthorizedPersonCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Profiles
{
    public class AuthorizedPersonCustomerConfirmationProfile : Profile
    {
        public AuthorizedPersonCustomerConfirmationProfile()
        {
            CreateMap<AuthorizedPersonCustomerConfirmation, AuthorizedPersonCustomerConfirmationDto>();
            CreateMap<AuthorizedPerson, AuthorizedPersonConfirmation>();
        }
    }
}
