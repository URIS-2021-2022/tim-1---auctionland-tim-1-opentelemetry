﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{
    /// <summary>
    /// DTO za ažuriranje žalbe
    /// </summary>
    public class ComplaintUpdateDto
    {
        /// <summary>
        /// Id žalbe koja se ažurira
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
        /// Datum donošenja rešenja
        /// </summary>
        public DateTime DateOfDecision { get; set; }

        /// <summary>
        /// Broj rešenja
        /// </summary>
        public string NumberOfDecision { get; set; }

        /// <summary>
        /// Akcija na osnovu žalbe
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
    }
}
