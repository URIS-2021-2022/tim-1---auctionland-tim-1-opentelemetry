using AutoMapper;
using CommissionMicroservice.Entities;
using CommissionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Profiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Member, MemberDto>();
            CreateMap<MemberCreationDto, Member>().ForAllMembers(opt => opt.Condition(src => src != null));
            CreateMap<MemberUpdateDto, Member>();
            CreateMap<Member, Member>();
        }
    }
}
