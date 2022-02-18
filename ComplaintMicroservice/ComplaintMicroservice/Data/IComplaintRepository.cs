using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Models;

namespace ComplaintMicroservice.Data
{
    public interface IComplaintRepository
    {
        List<Complaint> GetComplaints(string type, string status);

        Complaint GetComplaintById(Guid complaintId);
        
        ComplaintConfirmation CreateComplaint(Complaint complaint);

        void UpdateComplaint(Complaint complaint);

        void DeleteComplaint(Guid complaintId);

        bool SaveChanges();
    }
}
