using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ComplaintMicroservice.Models;

namespace ComplaintMicroservice.Entities
{
    /// <summary>
    /// Prestavlja žalbu
    /// </summary>
    public class Complaint
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
        /// Razlog podnosenja zalbe
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Obrazloženje žalbe
        /// </summary>
        public string Explanation { get; set; }
        
        /// <summary>
        /// Datum donošenja rešenja
        /// </summary>
        public DateTime DateOfDecision { get; set; }

        /// <summary>
        /// Broj rešenja
        /// </summary>
        public string NumberOfDecision { get; set; }

        /// <summary>
        /// Radnja sprovedena na osnovu žalbe
        /// </summary>
        public string Action { get; set; }

        #region
        /// <summary>
        /// Id tipa žabe
        /// </summary>
        public Guid ComplaintTypeId { get; set; }
        /// <summary>
        /// Tip žalbe
        /// </summary>
        public string ComplaintTypeName { get; set; }
        #endregion

        #region
        /// <summary>
        /// Id statusa žalbe
        /// </summary>
        public Guid ComplaintStatusId { get; set; }
        /// <summary>
        /// Status žalbe
        /// </summary>
        public string ComplaintStatus { get; set; }
        #endregion
    }
}
