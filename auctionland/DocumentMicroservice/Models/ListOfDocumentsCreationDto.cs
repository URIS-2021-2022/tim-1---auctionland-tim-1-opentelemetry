using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models
{
    public class ListOfDocumentsCreationDto : IValidatableObject
    {
        public DateTime ListCreationDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ListCreationDate.Equals("00.00.0000."))
            {
                yield return new ValidationResult(
                   "Nije moguće kreirati listu koja ima datum 00.00.0000.",
                   new[] { "ListOfDocumentsCreationDto" });
            }
        }
    }
}
