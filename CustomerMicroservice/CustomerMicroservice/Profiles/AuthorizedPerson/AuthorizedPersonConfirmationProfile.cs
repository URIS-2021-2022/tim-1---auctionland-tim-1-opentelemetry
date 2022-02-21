using AutoMapper;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models.AuthorizedPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Profiles
{
    public class AuthorizedPersonConfirmationProfile : Profile
    {
        public AuthorizedPersonConfirmationProfile()
        {
            CreateMap<AuthorizedPersonConfirmation, AuthorizedPersonConfirmationDto>();
            CreateMap<AuthorizedPerson, AuthorizedPersonConfirmation>();
        }
    }
}
