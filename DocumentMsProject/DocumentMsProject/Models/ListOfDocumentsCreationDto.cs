using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Models
{
    /// <summary>
    /// Model za kreiranje liste dokumenata
    /// </summary>
    public class ListOfDocumentsCreationDto
    {
        /// <summary>
        /// Datum kreiranja liste dokumenata
        /// </summary>
        public DateTime ListCreationDate { get; set; }
    }
}
