using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Data
{
    public interface ICommissionRepository
    {
        List<Commission> GetCommissions(string presidentID = null);
        Commission GetCommissionById(Guid commissionID);
        CommissionConfirmation CreateCommission(Commission commission);
        void UpdateCommission(Commission commission);
        void DeleteCommission(Guid commissionID);
        bool SaveChanges();
    }
}
