using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{
    public class ComplaintCreationDto
    {
        public DateTime SubmissionDate { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti razlog podnosenja zalbe!")]
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


    }
}
