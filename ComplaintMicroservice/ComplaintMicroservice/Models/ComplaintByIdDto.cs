using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{
    /// <summary>
    /// Dto za žalbu sa javnim nadmetanjem i kupcem
    /// </summary>
    public class ComplaintByIdDto
    {
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
        /// Datum donošenja rešenja
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
        /// Id tipa
        /// </summary>
        public Guid ComplaintTypeId { get; set; }
        /// <summary>
        /// Tip žalbe
        /// </summary>
        public string ComplaintTypeName { get; set; }
        #endregion

        #region
        /// <summary>
        /// Id statusa
        /// </summary>
        public Guid ComplaintStatusId { get; set; }
        /// <summary>
        /// Status žalbe
        /// </summary>
        public string ComplaintStatus { get; set; }
        #endregion

        /// <summary>
        /// Javno nadmetanje na koje se žalba odnosi
        /// </summary>
        public PublicBiddingDto PublicBidding { get; set; }

        /// <summary>
        /// Kupac koji je podneo žalbu
        /// </summary>
        public CustomerDto Customer { get; set; }
    }
}
