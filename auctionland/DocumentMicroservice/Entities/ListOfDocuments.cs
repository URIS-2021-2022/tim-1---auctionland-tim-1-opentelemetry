using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
    public class ListOfDocuments
    {
        /// <summary>
        /// ID liste dokumenata
        /// </summary>
        public Guid ListOfDocumentsId { get; set; }

        //public List<Document> documentsInList { get; set; }

        public DateTime ListCreationDate { get; set; }
    }
}
