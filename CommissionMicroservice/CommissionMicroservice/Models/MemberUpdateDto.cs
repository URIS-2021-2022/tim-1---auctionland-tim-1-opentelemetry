using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Models
{
    /// <summary>
    /// DTO za ažuriranje člana komisije
    /// </summary>
    public class MemberUpdateDto : IValidatableObject
    {
        public Guid MemberID { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti ime člana.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti prezime člana.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti funkciju člana.")]
        public string Role { get; set; }

        public Commission CommissionID { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
