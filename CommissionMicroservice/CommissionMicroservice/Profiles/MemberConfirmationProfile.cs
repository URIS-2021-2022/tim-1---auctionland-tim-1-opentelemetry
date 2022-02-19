using AutoMapper;
using CommissionMicroservice.Entities;
using CommissionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Profiles
{
    public class MemberConfirmationProfile : Profile
    {
        public MemberConfirmationProfile()
        {
            CreateMap<MemberConfirmation, MemberConfirmationDto>();
            CreateMap<Member, MemberConfirmation>();
        }
    }
}
