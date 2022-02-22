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
        /// <summary>
        /// Id korisnika
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Ime korisnika
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Prezime korisnika
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Korisnicko ime
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Email korisnika
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Lozinka korisnika
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Tip korisnika
        /// </summary>
        [ForeignKey("UserType")]
        public Guid? UserTypeId { get; set; }
        public UserType UserType { get; set; }

        /// <summary>
        /// Dokumenti korisnika
        /// </summary>
        [ForeignKey("Document")]
        public Guid DocumentId { get; set; }


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
