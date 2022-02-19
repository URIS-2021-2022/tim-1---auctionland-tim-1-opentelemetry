using AutoMapper;
using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Data
{
    public class CommissionRepository : ICommissionRepository
    {
        private readonly CommissionContext context;
        private readonly IMapper mapper;

        public CommissionRepository(IMapper mapper, CommissionContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public CommissionConfirmation CreateCommission(Commission commission)
        {
            var createdCommission = context.Add(commission);
            return mapper.Map<CommissionConfirmation>(createdCommission.Entity);
        }

        public void DeleteCommission(Guid commissionID)
        {
            var commission = GetCommissionById(commissionID);
            context.Remove(commission);
        }

        public Commission GetCommissionById(Guid commissionID)
        {
            return context.Commissions.FirstOrDefault(e => e.CommissionID == commissionID);
        }

        public List<Commission> GetCommissions(string presidentID = null)
        {
            return context.Commissions.Where(e => presidentID == null || e.President.MemberID == Guid.Parse(presidentID)).ToList();
        }

        public void UpdateCommission(Commission commission)
        {
            // ne mora nista
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
