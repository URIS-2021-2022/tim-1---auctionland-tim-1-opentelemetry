using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Models
{
    /// <summary>
    /// Model za kreiranje korisnika
    /// </summary>
    public class UserCreationDto : IValidatableObject
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti username.")]
        public string Username { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti password.")]
        public string Password { get; set; }

        public string Salt { get; set; }


        #region UserType
        public Guid UserTypeId { get; set; }

        public string UserTypeName { get; set; }
        #endregion

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName == LastName)
            {
                yield return new ValidationResult(
                    "Korisnik ne može da ima isto ime i prezime",
                    new[] { "UserCreationDto" });
            }
        }
        
    }
}
