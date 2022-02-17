using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using UserMicroservice.Entities;

namespace UserMicroservice.Data
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly UserContext context;
        private readonly IMapper mapper;

        public UserTypeRepository(UserContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<UserType> GetUserTypes()
        {
            return context.UserType.ToList();
        }

        public UserType GetUserTypeById(Guid userTypeId)
        {
            return context.UserType.FirstOrDefault (e => e.UserTypeId == userTypeId);
        }

        public UserTypeConfirmation CreateUserType(UserType userType)
        {
            var createdEntity = context.Add(userType);
            return mapper.Map<UserTypeConfirmation>(createdEntity.Entity);
        }                
        
        public void UpdateUserType(UserType userType)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteUserType(Guid userTypeId)
        {
            context.UserType.Remove(GetUserTypeById(userTypeId));
        }
    }
}
