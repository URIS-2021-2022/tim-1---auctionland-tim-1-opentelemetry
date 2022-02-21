using AutoMapper;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models.AuthorizedPersonCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Profiles
{
    public class AuthorizedPersonCustomerProfile : Profile
    {
        public AuthorizedPersonCustomerProfile()
        {
            CreateMap<AuthorizedPersonCustomer, AuthorizedPersonCustomerDto>();
            CreateMap<AuthorizedPersonCustomerCreationDto, AuthorizedPersonCustomer>();
            CreateMap<AuthorizedPersonCustomerUpdateDto, AuthorizedPersonCustomer>();
            CreateMap<AuthorizedPersonCustomer, AuthorizedPersonCustomer>();
        }
    }
}
