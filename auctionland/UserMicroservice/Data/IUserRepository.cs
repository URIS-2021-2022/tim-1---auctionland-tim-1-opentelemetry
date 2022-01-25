using System;
using System.Collections.Generic;
using UserMicroservice.Entities;

namespace UserMicroservice.Data
{
    interface IUserRepository
    {
        List<User> GetAll();

        User GetById(Guid id);

        User Create(User user);

        User Update(User user);

        void Delete(Guid id);
        bool UserWithCredentialsExists(string userName, string password);
    }
}
