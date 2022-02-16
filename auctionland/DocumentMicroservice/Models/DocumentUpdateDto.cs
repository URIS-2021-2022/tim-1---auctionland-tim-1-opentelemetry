﻿using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models
{
    public class DocumentUpdateDto
    {
        public Guid DocumentId { get; set; }
        public int DocumentSerialNumber { get; set; }
        public string DocumentName { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime DocumentSubmissionDate { get; set; }
        public string DocumentTemplate { get; set; }
        public ListOfDocuments ListOfDocuments { get; set; }
    }
}