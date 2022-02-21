using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Models
{
    /// <summary>
    /// Model za kreiranje dokumenta
    /// </summary>
    public class DocumentCreationDto
    {
        /// <summary>
        /// serijski broj
        /// </summary>
        public int DocumentSerialNumber { get; set; }

        /// <summary>
        /// naziv dokumenta
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// datum izdavanja dokumenta
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// datum podnosenja dokumenta
        /// </summary>
        public DateTime DocumentSubmissionDate { get; set; }

        #region ListOfDocuments
        /// <summary>
        /// ID liste dokumenata
        /// </summary>
        public Guid ListOfDocumentsID { get; set; }
        #endregion
    }
}
