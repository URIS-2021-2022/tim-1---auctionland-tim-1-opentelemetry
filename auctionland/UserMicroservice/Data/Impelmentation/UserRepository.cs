using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Entities;

namespace UserMicroservice.Data.Impelmentation
{
    public class UserRepository : IUserRepository
    {
        public List<User> users = new List<User>();

        public User Create(User user)
        {
            user.Id = Guid.NewGuid();
            users.Add(user);
            return user;
        }

        public void Delete(Guid id)
        {
            users.Remove(users.FirstOrDefault(e => e.Id == id));
        }

        public List<User> GetAll()
        {
            return users;
        }

        public User GetById(Guid id)
        {
            return users.FirstOrDefault(e => e.Id == id);
        }

        public User Update(User user)
        {
            return user;
        }
    }
}
