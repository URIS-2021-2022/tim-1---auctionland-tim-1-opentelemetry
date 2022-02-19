using AutoMapper;
using CommissionMicroservice.Entities;
using CommissionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Profiles
{
    public class CommissionProfile : Profile
    {
        public CommissionProfile()
        {
            CreateMap<Commission, CommissionDto>();
            CreateMap<CommissionCreationDto, Commission>();
            CreateMap<CommissionUpdateDto, Commission>();
            CreateMap<Commission, Commission>();
        }
    }
}
