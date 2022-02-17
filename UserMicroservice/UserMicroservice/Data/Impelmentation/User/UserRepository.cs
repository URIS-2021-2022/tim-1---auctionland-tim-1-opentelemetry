using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using UserMicroservice.Entities;

namespace UserMicroservice.Data.Impelmentation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;
        private readonly IMapper mapper;

        public UserRepository(UserContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<User> GetUsers(string firstName = null, string lastName = null)
        {
            return context.User.Where(e => (firstName == null || e.FirstName == firstName) &&
                                                        (lastName == null || e.LastName == lastName)).ToList();
        }

        public User GetUserById(Guid userId)
        {
            return context.User.FirstOrDefault(e => e.UserId == userId);
        }

        public UserConfirmation CreateUser(User user)
        {
            var createdEntity = context.Add(user);
            return mapper.Map<UserConfirmation>(createdEntity.Entity);
        }

        public void UpdateUser(User user)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteUser(Guid userId)
        {
            context.Remove(GetUserById(userId));
        }
    }
}
