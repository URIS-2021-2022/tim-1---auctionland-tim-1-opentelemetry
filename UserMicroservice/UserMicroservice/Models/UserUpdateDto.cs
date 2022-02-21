using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserMicroservice.Entities;

namespace UserMicroservice.Models
{
    /// <summary>
    /// Model za ažuriranje korisnika
    /// </summary>
    public class UserUpdateDto : IValidatableObject
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti username korisnika.")]
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        [ForeignKey("UserType")]
        public Guid? UserTypeId { get; set; }
        public UserType UserType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
       
    }
}
