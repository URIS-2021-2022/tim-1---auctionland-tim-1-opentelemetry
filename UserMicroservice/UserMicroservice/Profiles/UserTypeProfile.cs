using AutoMapper;
using UserMicroservice.Entities;
using UserMicroservice.Models;

namespace UserMicroservice.Profiles
{
    public class UserTypeProfile : Profile
    {
        public UserTypeProfile()
        {
            CreateMap<UserType, UserTypeDto>();
            CreateMap<UserTypeCreationDto, UserType>();
            CreateMap<UserTypeUpdateDto, UserType>();
            CreateMap<UserType, UserType>();
        }
    }
}

