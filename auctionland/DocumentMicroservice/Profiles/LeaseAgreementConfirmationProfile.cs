using AutoMapper;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Profiles
{
    public class LeaseAgreementConfirmationProfile : Profile
    {
        public LeaseAgreementConfirmationProfile()
        {
            CreateMap<LeaseAgreementConfirmation, LeaseAgreementConfirmationDto>();
        }
    }
}
