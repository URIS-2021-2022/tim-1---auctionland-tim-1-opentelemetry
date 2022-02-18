using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{
    public class CompaintModel
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
        /// Obrazloženje žalbe
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// Datum rešenja
        /// </summary>
        public DateTime DateOfDecision { get; set; }

        /// <summary>
        /// Broj rešenja
        /// </summary>
        public string NumberOfDecision { get; set; }

        /// <summary>
        /// Akcija sprovedena na osnovu žalbe
        /// </summary>
        public string Action { get; set; }

        #region
        /// <summary>
        /// Id tipa žalbe
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
