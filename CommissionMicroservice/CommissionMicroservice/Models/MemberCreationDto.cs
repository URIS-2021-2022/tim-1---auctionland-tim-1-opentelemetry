using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Models
{
    /// <summary>
    /// DTO za kreiranje člana komisije
    /// </summary>
    public class MemberCreationDto : IValidatableObject
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
            if (FirstName == LastName)
            {
                yield return new ValidationResult(
                    "Korisnik ne može da ima isto ime i prezime!",
                    new[] { "MemberCreationDto" });
            }
        }
    }
}
