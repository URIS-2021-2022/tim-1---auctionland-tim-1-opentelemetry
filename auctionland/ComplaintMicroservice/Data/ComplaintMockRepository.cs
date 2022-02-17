using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Models;

namespace ComplaintMicroservice.Data
{
    public class ComplaintMockRepository : IComplaintRepository
    {
        public static List<Complaint> Complaints { get; set; } = new List<Complaint>();
         
        public ComplaintMockRepository()
        {
            FillData();
        }

        private static void FillData()
        {
            Complaints.AddRange(new List<Complaint>
            {
                new Complaint
                {
                    ComplaintId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ComplaintStatus = "Rejected",
                    ComplaintStatusId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    Action = "Public bidding does not go to the second round",
                    DateOfDecision = DateTime.Parse("2020-11-15T09:00:00"),
                    Explanation = "Explanation",
                    NumberOfDecision = "1",
                    Reason = "Reason",
                    SubmissionDate = DateTime.Parse("2020-11-13T09:00:00"),
                    ComplaintTypeName = "Appeal against the course of the public tender",
                    ComplaintTypeId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f")
                },
                new Complaint
                {
                     ComplaintId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    ComplaintStatus = "Rejected",
                    ComplaintStatusId = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    Action = "Public bidding does not go to the second round",
                    DateOfDecision = DateTime.Parse("2020-11-15T09:00:00"),
                    Explanation = "Explanation",
                    NumberOfDecision = "2",
                    Reason = "Reason",
                    SubmissionDate = DateTime.Parse("2020-11-13T09:00:00"),
                    ComplaintTypeName = "Appeal against the Decision on Leasing",
                    ComplaintTypeId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3")
                }
            });
        }

        public List<Complaint> GetComplaints(string type, string status)
        {
            return (from e in Complaints
                    where string.IsNullOrEmpty(type) || e.ComplaintTypeName == type
                    select e).ToList();
        }

        public Complaint GetComplaintById(Guid complaintId)
        {
            return Complaints.FirstOrDefault(c => c.ComplaintId == complaintId);
        }

        public ComplaintConfirmation CreateComplaint(Complaint complaint)
        {
            complaint.ComplaintId = Guid.NewGuid();
            Complaints.Add(complaint);
            Complaint com = GetComplaintById(complaint.ComplaintId);

            return new ComplaintConfirmation
            {
                ComplaintId = com.ComplaintId,
                SubmissionDate = com.SubmissionDate,
                Action = com.Action,
                ComplaintStatus = com.ComplaintStatus,
                ComplaintTypeName = com.ComplaintTypeName,
                Reason = com.Reason
            };
        }

        public void DeleteComplaint(Guid complaintId)
        {
            Complaints.Remove(Complaints.FirstOrDefault(c => c.ComplaintId == complaintId));
        }

        public void UpdateComplaint(Complaint complaint)
        {
            Complaint model = GetComplaintById(complaint.ComplaintId);

            model.ComplaintId = complaint.ComplaintId;
            model.ComplaintStatus = complaint.ComplaintStatus;
            model.ComplaintStatusId = complaint.ComplaintStatusId;
            model.Action = complaint.Action;
            model.DateOfDecision = complaint.DateOfDecision;
            model.Explanation = complaint.Explanation;
            model.NumberOfDecision = complaint.NumberOfDecision;
            model.Reason = complaint.Reason;
            model.SubmissionDate = complaint.SubmissionDate;
            model.ComplaintTypeId = complaint.ComplaintTypeId;
            model.ComplaintTypeName = complaint.ComplaintTypeName;

            /*return new ComplaintConfirmation
            {
                ComplaintId = model.ComplaintId,
                SubmissionDate = model.SubmissionDate,
                Action = model.Action,
                ComplaintStatus = model.ComplaintStatus,
                ComplaintTypeName = model.ComplaintTypeName,
                Reason = model.Reason
            };*/
        }

        public List<Complaint> GetComplaints()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
