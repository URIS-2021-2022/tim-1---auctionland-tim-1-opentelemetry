using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
    public class Document
    {
        /// <summary>
        /// ID dokumenta
        /// </summary>
        [Key]
        public Guid DocumentId { get; set; }

        /// <summary>
        /// serijski broj
        /// </summary>
        [Column("int")]
        public int DocumentSerialNumber { get; set; }

        /// <summary>
        /// naziv dokumenta
        /// </summary>
        [Column("nvarchar(250)")]
        public string DocumentName { get; set; }

        /// <summary>
        /// datum izdavanja dokumenta
        /// </summary>
        [Column("date")]
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// datum podnosenja dokumenta
        /// </summary>
        [Column("date")]
        public DateTime DocumentSubmissionDate { get; set; }

        /// <summary>
        /// sablon dokumenta.
        /// </summary>
        [Column("nvarchar(250)")]
        public string DocumentTemplate { get; set; }

        /// <summary>
        /// ID liste dokumenata
        /// </summary>
        [Required]
        //[ForeignKeyAttribute("ListOfDocuments")]
        [ForeignKey("ListOfDocuments")]
        public Guid ListOfDocumentsID { get; set; }
        public ListOfDocuments ListOfDocuments { get; set; }
    }
}
