using DocumentMsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.ServiceCalls.CommissionMicroservice
{
    public interface ICommissionMicroservice
    {
        public bool GetMembers(CommissionDto commissionDto);
    }
}
