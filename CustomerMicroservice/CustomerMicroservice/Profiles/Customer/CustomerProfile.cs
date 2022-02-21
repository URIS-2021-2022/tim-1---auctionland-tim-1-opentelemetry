using AutoMapper;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerCreationDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>();
            CreateMap<Customer, Customer>();
        }
    }
}
