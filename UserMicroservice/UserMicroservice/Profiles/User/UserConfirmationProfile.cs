using AutoMapper;
using UserMicroservice.Entities;
using UserMicroservice.Models;

namespace UserMicroservice.Profiles
{
    public class UserConfirmationProfile : Profile
    {
        public UserConfirmationProfile()
        {
            CreateMap<UserConfirmation, UserConfirmationDto>();
            CreateMap<User, UserConfirmation>();
        }
    }
}

