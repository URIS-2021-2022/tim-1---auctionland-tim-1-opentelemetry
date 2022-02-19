using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Models
{
    /// <sumary>
    /// DTO za clana komisije
    /// </sumary>
    public class MemberDto
    {
        public Guid MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        //[ForeignKey("Commission")]
        public Commission CommissionID { get; set; }
    }
}
