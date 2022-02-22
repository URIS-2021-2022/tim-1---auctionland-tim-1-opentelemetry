using AutoMapper;
using DocumentMsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Profiles
{
    public class CommissionProfile : Profile
    {
        public CommissionProfile()
        {
            CreateMap<CommissionDto, LeaseAgreementDto>().ForMember(dest => dest.MinisterID, opt => opt.MapFrom(src => $"{src.MemberID}"));
        }
    }
}
