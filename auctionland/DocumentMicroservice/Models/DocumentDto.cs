using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models
{
    public class DocumentDto
    {
        public Guid DocumentId { get; set; }
        public int DocumentSerialNumber { get; set; }
        public string DocumentName { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime DocumentSubmissionDate { get; set; }
        public string DocumentTemplate { get; set; }

        //TODO: treba da proverim da li ce u bazi da upise samo kao id zbog mapiranja upisati ceo objekat ili ce da upise samo objekat
        public ListOfDocuments ListOfDocuments { get; set; }
    }
}
