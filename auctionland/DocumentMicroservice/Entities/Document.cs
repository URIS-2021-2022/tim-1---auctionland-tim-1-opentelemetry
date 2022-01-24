using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
    public class Document
    {
        /// <summary>
        /// ID dokumenta
        /// </summary>
        public Guid DocumentId { get; set; }

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

        /// <summary>
        /// sablon dokumenta.
        /// </summary>
        public string DocumentTemplate { get; set; }

        /// <summary>
        /// ID liste dokumenata
        /// </summary>
        public ListOfDocuments ListOfDocumentsId { get; set; }
    }
}
