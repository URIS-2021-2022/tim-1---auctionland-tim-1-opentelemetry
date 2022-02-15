using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Models
{
    public class DocumentCreationDto : IValidatableObject
    {
        public int DocumentSerialNumber { get; set; }
        public string DocumentName { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime DocumentSubmissionDate { get; set; }
        public string DocumentTemplate { get; set; }
        public ListOfDocuments ListOfDocuments { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(DocumentSerialNumber == 0)
            {
                yield return new ValidationResult(
                   "Nije moguće kreirati dokument koji ima serijski broj 0.",
                   new[] { "DocumentCreationDto" });
            }
            else if (DocumentName == null)
            {
                yield return new ValidationResult(
                   "Nije moguće kreirati dokument koji nema naziv.",
                   new[] { "DocumentCreationDto" });
            }
            else if (DocumentDate < DocumentSubmissionDate)
            {
                yield return new ValidationResult(
                   "Nije moguće kreirati dokument koji je kreiran nakon datuma podnosenja.",
                   new[] { "DocumentCreationDto" });
            }
        }
    }
}
