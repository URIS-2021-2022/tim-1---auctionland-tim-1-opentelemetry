using System;
using System.Collections.Generic;
using UserMicroservice.Entities;

namespace UserMicroservice.Data
{
    public interface IUserRepository
    {
        List<User> GetUsers(string firstName = null, string lastName = null);

        User GetUserById(Guid id);

        UserConfirmation CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(Guid id);

        bool SaveChanges();
    }
}
