using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserMicroservice.Models
{
    /// <summary>
    /// Model za potvrdu tipa korisnika
    /// </summary>
    public class UserTypeConfirmationDto 
    {
        public Guid UserTypeId { get; set; }

        public string UserTypeName { get; set; }
    }
}
