﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models
{
    public class ListOfDocumentsConfirmationDto
    {
        public Guid ListOfDocumentsId { get; set; }

        //public List<Document> documentsInList { get; set; }

        public DateTime ListCreationDate { get; set; }
    }
}