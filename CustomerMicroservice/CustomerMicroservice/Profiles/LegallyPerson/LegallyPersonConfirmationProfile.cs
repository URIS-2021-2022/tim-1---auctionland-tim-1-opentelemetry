using AutoMapper;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models.LegallyPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Profiles
{
    public class LegallyPersonConfirmationProfile : Profile
    {
        public LegallyPersonConfirmationProfile()
        {
            CreateMap<LegallyPersonConfirmation, LegallyPersonConfirmationDto>();
            CreateMap<LegallyPerson, LegallyPersonConfirmation>();
        }
    }
}
