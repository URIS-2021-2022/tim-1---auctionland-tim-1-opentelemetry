using System;
using System.Collections.Generic;
using PublicBiddingMicroservice.Entities;

namespace PublicBiddingMicroservice.Data
{
    public interface IUserAuthRepository
    {
        bool UserWithCredentialsExists(string username, string password);
    }
}
