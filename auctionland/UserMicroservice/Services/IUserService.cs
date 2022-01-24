using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Models;
using UserMicroservice.Entities;


namespace UserMicroservice.Services
{
    interface IUserService
    {
        List<UserResponseDto> GetAll();
        
        UserResponseDto GetById(Guid id);

        UserResponseDto Create(UserRequestDto requestDTO);

        UserResponseDto Update(UserRequestDto requestDTO);

        void Delete(Guid id);

    }

}
