using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Data
{
    public interface IUserRepository
    {
        bool UserWithCredentialsExists(string username, string password);
    }
}
