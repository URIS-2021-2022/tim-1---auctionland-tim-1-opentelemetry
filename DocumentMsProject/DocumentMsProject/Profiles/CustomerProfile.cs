using AutoMapper;
using DocumentMsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, LeaseAgreementDto>().ForMember(dest => dest.PersonID, opt => opt.MapFrom(src => $"{src.CustomerID}"));
        }
    }
}
