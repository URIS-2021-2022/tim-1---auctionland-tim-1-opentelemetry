using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserMicroservice.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Column("varchar(60)")]
        public string FirstName { get; set; }

        [Column("varchar(60)")]
        public string LastName { get; set; }

        [Column("varchar(60)")]
        public string UserName { get; set; }

        [Column("varchar(60)")]
        public string Email { get; set; }

        [Column("varchar(20)")]
        public string Password { get; set; }

        [ForeignKey("UserType")]
        public Guid userId { get; set; }

        public UserType userType { get; set; }

    }
}
