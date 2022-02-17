using System;
using System.Collections.Generic;
using UserMicroservice.Entities;

namespace UserMicroservice.Data
{
    public interface IUserTypeRepository
    {
        List<UserType> GetUserTypes();

        UserType GetUserTypeById(Guid userTypeId);

        UserTypeConfirmation CreateUserType(UserType userType);

        void UpdateUserType(UserType userType);

        void DeleteUserType(Guid userTypeId);

        bool SaveChanges();
    }
}
