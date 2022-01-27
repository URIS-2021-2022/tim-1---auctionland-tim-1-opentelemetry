using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models
{
    public class RequestListOfDocumentsDto
    {
        [Required]
        public DateTime ListCreationDate { get; set; }
    }
}
