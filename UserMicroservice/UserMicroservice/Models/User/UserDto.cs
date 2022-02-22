using System;
using System.ComponentModel.DataAnnotations.Schema;
using UserMicroservice.Entities;

namespace UserMicroservice.Models
{
    /// <summary>
    /// DTO za korisnika
    /// </summary>
    public class UserDto
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        [ForeignKey("UserType")]
        public Guid? UserTypeId { get; set; }
        public UserType UserType { get; set; }

        [ForeignKey("Document")]
        public Guid? DocumentId { get; set; }

    }
}
