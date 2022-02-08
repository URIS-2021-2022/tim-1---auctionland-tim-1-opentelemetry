using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities
{
    public class ComplaintConfirmation
    {
        public Guid ComplaintId { get; set; }

        public DateTime SubmissionDate { get; set; }

        public string Reason { get; set; }

        public string Explanation { get; set; }

        public DateTime DateOfDecision { get; set; }

        public string NumberOfDecision { get; set; }

        public string Action { get; set; }

        public string ComplaintTypeName { get; set; }

        public string ComplaintStatus { get; set; }
    }
}
