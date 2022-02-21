using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Entities
{
    /// <summary>
    /// Predstavlja listu dokumenata.
    /// </summary>
    public class ListOfDocuments
    {
        /// <summary>
        /// ID liste dokumenata
        /// </summary>
        [Key]
        public Guid ListOfDocumentsId { get; set; }

        /// <summary>
        /// Datum kreiranja liste dokumenata
        /// </summary>
        public DateTime ListCreationDate { get; set; }
    }
}
