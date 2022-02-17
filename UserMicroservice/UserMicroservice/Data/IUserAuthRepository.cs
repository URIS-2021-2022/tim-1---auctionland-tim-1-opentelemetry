using System;
using System.Collections.Generic;
using UserMicroservice.Entities;

namespace UserMicroservice.Data
{
    public interface IUserAuthRepository
    {
        bool UserWithCredentialsExists(string username, string password);
    }
}
