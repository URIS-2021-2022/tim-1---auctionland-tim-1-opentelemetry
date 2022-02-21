using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Models
{
    /// <summary>
    /// DTO liste dokumenata
    /// </summary>
    public class ListOfDocumentsDto
    {
        /// <summary>
        /// ID liste dokumenata
        /// </summary>
        public Guid ListOfDocumentsId { get; set; }

        /// <summary>
        /// Datum kreiranja liste dokumenata
        /// </summary>
        public DateTime ListCreationDate { get; set; }
    }
}
