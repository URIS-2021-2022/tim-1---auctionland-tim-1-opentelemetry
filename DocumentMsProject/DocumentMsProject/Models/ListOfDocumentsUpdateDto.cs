using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Models
{
    public class ListOfDocumentsUpdateDto
    {
        public Guid ListOfDocumentsId { get; set; }

        public DateTime ListCreationDate { get; set; }
    }
}
