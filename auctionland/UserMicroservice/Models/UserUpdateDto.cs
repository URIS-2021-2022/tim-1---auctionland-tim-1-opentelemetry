using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserMicroservice.Models
{
    /// <summary>
    /// Model za ažuriranje prijave ispita
    /// </summary>
    public class UserUpdateDto : IValidatableObject
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti username korisnika.")]
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        #region UserType
        public Guid UserTypeId { get; set; }

        public string UserTypeName { get; set; }
        #endregion

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
       
    }
}
