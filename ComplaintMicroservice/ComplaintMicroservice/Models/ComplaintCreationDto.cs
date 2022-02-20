using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Models
{
    /// <summary>
    /// Model za kreiranje žalbe
    /// </summary>
    public class ComplaintCreationDto
    {
        /// <summary>
        /// Datum podnošenja žalbe
        /// </summary>
        public DateTime SubmissionDate { get; set; }

        /// <summary>
        /// Razlog podnošenja žalbe koji je obavezno uneti
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti razlog podnosenja zalbe!")]
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
