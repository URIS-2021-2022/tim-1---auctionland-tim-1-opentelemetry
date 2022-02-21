using AutoMapper;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models.PhysicalPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Profiles
{
    public class PhysicalPersonProfile : Profile
    {
        public PhysicalPersonProfile()
        {
            CreateMap<PhysicalPerson, PhysicalPersonDto>();
            CreateMap<PhysicalPersonCreationDto, PhysicalPerson>();
            CreateMap<PhysicalPersonUpdateDto, PhysicalPerson>();
            CreateMap<PhysicalPerson, PhysicalPerson>();
        }
    }
}
