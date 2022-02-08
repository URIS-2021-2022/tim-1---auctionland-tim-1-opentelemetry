using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{
    public class ComplaintUpdateDto
    {
        public Guid ComplaintId { get; set; }

        public DateTime SubmissionDate { get; set; }

        public string Reason { get; set; }

        public string Explanation { get; set; }

        public DateTime DateOfDecision { get; set; }

        public string NumberOfDecision { get; set; }

        public string Action { get; set; }

        #region
        public Guid ComplaintTypeId { get; set; }
        public string ComplaintTypeName { get; set; }
        #endregion

        #region
        public Guid ComplaintStatusId { get; set; }
        public string ComplaintStatus { get; set; }
        #endregion

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(DateOfDecision < SubmissionDate)
            {
                yield return new ValidationResult
                    ("Nije moguce da datum donosenja resenja na osnovu zalbe bude pre datuma podnosenja zalbe.", new[] { "ComplaintUpdateDto" });
            }
        }
  
    }
}
