using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComplaintMicroservice.Entities;

namespace ComplaintMicroservice.Data
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ComplaintContext context;
        private readonly IMapper mapper;

        public ComplaintRepository(ComplaintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Perzistira sve izmene u bazi
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public ComplaintConfirmation CreateComplaint(Complaint complaint)
        {
            complaint.ComplaintId = Guid.NewGuid();
            var ceatedEntity = context.Add(complaint);
            return mapper.Map<ComplaintConfirmation>(ceatedEntity.Entity);
        }

        public void DeleteComplaint(Guid complaintId)
        {
            var complaint = GetComplaintById(complaintId);
            context.Remove(complaint);
        }

        public Complaint GetComplaintById(Guid complaintId)
        {
            return context.Complaints.FirstOrDefault(c => c.ComplaintId == complaintId);
        }

        public List<Complaint> GetComplaints(string type, string status)
        {
            return context.Complaints.Where(e => (type == null || e.ComplaintTypeName == type) &&
                                                 (status == null || e.ComplaintStatus == status)).ToList();
        }

        public void UpdateComplaint(Complaint complaint)
        {
            
        }
    }
}
