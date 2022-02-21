using AutoMapper;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models.LegallyPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Profiles
{
    public class LegallyPersonProfile : Profile
    {
        public LegallyPersonProfile()
        {
            CreateMap<LegallyPerson, LegallyPersonDto>();
            CreateMap<LegallyPersonCreationDto, LegallyPerson>();
            CreateMap<LegallyPersonUpdateDto, LegallyPerson>();
            CreateMap<LegallyPerson, LegallyPerson>();
        }
    }
}
