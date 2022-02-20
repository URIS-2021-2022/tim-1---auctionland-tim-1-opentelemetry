using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Models
{
    /// <summary>
    /// DTO za kreiranje člana komisije
    /// </summary>
    public class MemberCreationDto : IValidatableObject
    {
        /// <summary>
        /// ID clana komisije
        /// </summary>
        [Required]
        public Guid MemberID { get; set; }

        /// <summary>
        /// Ime clana komisije
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti ime člana.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Prezime clana komisije
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti prezime člana.")]
        public string LastName { get; set; }

        /// <summary>
        /// Funkcija clana komisije
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti funkciju člana.")]
        public string Role { get; set; }

        /// <summary>
        /// ID komisije
        /// </summary>
        public Guid? CommissionID { get; set; }
        /// <summary>
        /// Naziv komisije
        /// </summary>
        public string NameCommission { get; set; }

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
