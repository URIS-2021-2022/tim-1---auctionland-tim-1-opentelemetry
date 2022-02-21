using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Entities
{
    /// <summary>
    /// Predstavlja potvrdu liste dokumenata.
    /// </summary>
    public class ListOfDocumentsConfirmation
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
