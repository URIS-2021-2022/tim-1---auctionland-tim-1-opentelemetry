using System;
using System.Collections.Generic;
using UserMicroservice.Entities;

namespace UserMicroservice.Data
{
    public interface IUserRepository
    {
        List<User> GetUsers(string firstName = null, string lastName = null);

        User GetUserById(Guid userId);

        UserConfirmation CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(Guid userId);

        bool SaveChanges();
    }
}
