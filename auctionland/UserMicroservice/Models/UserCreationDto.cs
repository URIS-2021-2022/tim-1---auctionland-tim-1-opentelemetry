using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserMicroservice.Entities;

namespace UserMicroservice.Models
{
    /// <summary>
    /// Model za kreiranje korisnika
    /// </summary>
    public class UserCreationDto : IValidatableObject
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti username.")]
        public string Username { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti password.")]
        public string Password { get; set; }

        public string Salt { get; set; }

        [ForeignKey("UserType")]
        public Guid? UserTypeId { get; set; }
        public UserType UserType { get; set; }


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
