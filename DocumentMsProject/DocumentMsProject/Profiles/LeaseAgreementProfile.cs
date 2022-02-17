using AutoMapper;
using DocumentMsProject.Entities;
using DocumentMsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Profiles
{
    public class LeaseAgreementProfile : Profile
    {
        public LeaseAgreementProfile()
        {
            CreateMap<LeaseAgreement, LeaseAgreementDto>();
            CreateMap<LeaseAgreementCreationDto, LeaseAgreement>();
            CreateMap<LeaseAgreementUpdateDto, LeaseAgreement>();
            CreateMap<LeaseAgreement, LeaseAgreement>();
        }
    }
}
