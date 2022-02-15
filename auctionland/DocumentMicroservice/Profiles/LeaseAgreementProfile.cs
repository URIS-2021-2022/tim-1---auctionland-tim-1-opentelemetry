using AutoMapper;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Profiles
{
    public class LeaseAgreementProfile : Profile
    {
        public LeaseAgreementProfile() 
        {
            CreateMap<LeaseAgreement, LeaseAgreementDto>();
            CreateMap<LeaseAgreement, LeaseAgreementCreationDto>();
            CreateMap<LeaseAgreement, LeaseAgreementUpdateDto>();
        }
    }
}
