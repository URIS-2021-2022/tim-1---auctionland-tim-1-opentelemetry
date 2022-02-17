using AutoMapper;
using UserMicroservice.Entities;
using UserMicroservice.Models;

namespace UserMicroservice.Profiles
{
    public class UserTypeConfirmationProfile : Profile
    {
        public UserTypeConfirmationProfile()
        {
            CreateMap<UserTypeConfirmation, UserTypeConfirmationDto>();
            CreateMap<UserType, UserTypeConfirmation>();
        }
    }
}

