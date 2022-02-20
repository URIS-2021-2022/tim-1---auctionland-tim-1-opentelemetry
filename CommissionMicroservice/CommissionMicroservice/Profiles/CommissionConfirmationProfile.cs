using AutoMapper;
using CommissionMicroservice.Entities;
using CommissionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Profiles
{
    public class CommissionConfirmationProfile : Profile
    {
        public CommissionConfirmationProfile()
        {
            CreateMap<CommissionConfirmation, CommissionConfirmationDto>();
            CreateMap<Commission, CommissionConfirmation>();
        }
    }
}
