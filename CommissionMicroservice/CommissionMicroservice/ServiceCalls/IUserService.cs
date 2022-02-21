using CommissionMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.ServiceCalls
{
    public interface IUserService
    {
        public bool GetUserOfCommission(UserDto userDto);
    }
}
