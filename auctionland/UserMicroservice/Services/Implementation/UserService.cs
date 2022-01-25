using System;
using System.Collections.Generic;
using UserMicroservice.Data;
using UserMicroservice.Entities;
using UserMicroservice.Models;

namespace UserMicroservice.Services.Implementation
{
    public class UserService : IUserService
    {
        IUserRepository userRepository;
        public UserResponseDto Create(UserRequestDto requestDTO)
        {
            User user = new User()
            {
                FirstName = requestDTO.FirstName,
                LastName = requestDTO.LastName,
                UserName = requestDTO.UserName,
                Email = requestDTO.Email,
                Password = requestDTO.Password,
            };

            userRepository.Create(user);

            UserResponseDto responseDto = new UserResponseDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
            };

            return responseDto;
        }

        public List<UserResponseDto> GetAll()
        {
            List<User> users = userRepository.GetAll();
            List<UserResponseDto> userResponseDtos = new List<UserResponseDto>();
           
            foreach(User user in users)
            {
                UserResponseDto responseDto = new UserResponseDto()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                };
                userResponseDtos.Add(responseDto);
            }
         

            return userResponseDtos;
        }

        public UserResponseDto GetById(Guid id)
        {

            User user = userRepository.GetById(id);

            UserResponseDto responseDto = new UserResponseDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
            };

            return responseDto;
        }

        public UserResponseDto Update(UserRequestDto requestDTO)
        {

            User user = new User()
            {
                FirstName = requestDTO.FirstName,
                LastName = requestDTO.LastName,
                UserName = requestDTO.UserName,
                Email = requestDTO.Email,
                Password = requestDTO.Password,
            };

            userRepository.Update(user);

            UserResponseDto responseDto = new UserResponseDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
            };

            return responseDto;
        }

        public void Delete(Guid id)
        {
            userRepository.Delete(id);
        }

    }
}
