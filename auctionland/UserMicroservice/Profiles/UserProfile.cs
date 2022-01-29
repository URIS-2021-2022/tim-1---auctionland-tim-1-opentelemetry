using AutoMapper;
using UserMicroservice.Entities;
using UserMicroservice.Models;

namespace UserMicroservice.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserCreationDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, User>();
        }
    }
}
