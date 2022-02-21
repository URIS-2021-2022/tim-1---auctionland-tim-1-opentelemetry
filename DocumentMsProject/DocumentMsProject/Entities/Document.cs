using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Entities
{
    /// <summary>
    /// Predstavlja dokument u vezi sa javnim nadmetanjem
    /// </summary>
    public class Document
    {
        /// <summary>
        /// ID dokumenta.
        /// </summary>
        [Key]
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
        [Required]
        //[ForeignKeyAttribute("ListOfDocuments")]
        //[ForeignKey("ListOfDocuments")]
        #region ListOfDocuments
        public Guid ListOfDocumentsID { get; set; }
        #endregion
    }
}
