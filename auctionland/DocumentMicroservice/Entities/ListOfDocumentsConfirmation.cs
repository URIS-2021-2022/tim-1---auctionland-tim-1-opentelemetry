using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Entities
{
    public class ListOfDocumentsConfirmation
    {
        public Guid ListOfDocumentsId { get; set; }

        public DateTime ListCreationDate { get; set; }
    }
}
