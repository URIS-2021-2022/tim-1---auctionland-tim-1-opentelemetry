using AutoMapper;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models.PhysicalPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Profiles
{
    public class PhysicalPersonConfirmationProfile : Profile
    {
        public PhysicalPersonConfirmationProfile()
        {
            CreateMap<PhysicalPersonConfirmation, PhysicalPersonConfirmationDto>();
            CreateMap<PhysicalPerson, PhysicalPersonConfirmation>();
        }
    }
}
