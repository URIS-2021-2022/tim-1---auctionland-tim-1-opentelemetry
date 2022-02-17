using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Entities
{
    public class ListOfDocuments
    {
        /// <summary>
        /// ID liste dokumenata
        /// </summary>
        [Key]
        public Guid ListOfDocumentsId { get; set; }

        public DateTime ListCreationDate { get; set; }
    }
}
