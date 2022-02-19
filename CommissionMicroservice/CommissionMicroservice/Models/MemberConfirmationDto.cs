using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Models
{
    /// <summary>
    /// DTO za potvrdu clana komisije
    /// </summary>
    public class MemberConfirmationDto
    {
        public Guid MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public Commission CommissionID { get; set; }
    }
}
