using AutoMapper;
using CommissionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CommissionCreationDto, UserDto>();
        }
    }
}
