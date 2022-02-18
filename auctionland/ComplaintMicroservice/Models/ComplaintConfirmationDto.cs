using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{   
    /// <summary>
    /// DTO za potvrdu kreiranja žalbe
    /// </summary>
    public class ComplaintConfirmationDto
    {
        /// <summary>
        /// Id žalbe
        /// </summary>
        public Guid ComplaintId { get; set; }

        /// <summary>
        /// Datum podnošenja žalbe
        /// </summary>
        public DateTime SubmissionDate { get; set; }

        /// <summary>
        /// Razlog podnošenja žalbe
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Radnja koja se sprovodi na osnovu žalbe
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Tip žalbe
        /// </summary>
        public string ComplaintTypeName { get; set; }

        /// <summary>
        /// Status žalbe
        /// </summary>
        public string ComplaintStatus { get; set; }
    }
}
