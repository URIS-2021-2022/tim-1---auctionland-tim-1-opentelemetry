using System;
using System.Collections.Generic;
using System.Linq;
using UserMicroservice.Entities;

namespace UserMicroservice.Data.Impelmentation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;

        public User Create(User user)
        {
            user.Id = Guid.NewGuid();
            context.Users.Add(user);
            return user;
        }

        public void Delete(Guid id)
        {
            context.Users.Remove(context.Users.FirstOrDefault(e => e.Id == id));
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetById(Guid id)
        {
            return context.Users.FirstOrDefault(e => e.Id == id);
        }

        public User Update(User user)
        {
            return user;
        }

        public bool UserWithCredentialsExists(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
