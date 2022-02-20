using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities
{
    /// <summary>
    /// Potvrda kreiranja žalbe
    /// </summary>
    public class ComplaintConfirmation
    {
        /// <summary>
        /// Id žalbe
        /// </summary>
        public Guid ComplaintId { get; set; }

        /// <summary>
        /// Datum podnošenja
        /// </summary>
        public DateTime SubmissionDate { get; set; }

        /// <summary>
        /// Razlog podnošenja
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Radnja sprovedena na osnovu žalbe
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Tip
        /// </summary>
        public string ComplaintTypeName { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string ComplaintStatus { get; set; }
    }
}
