using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Models
{
    /// <summary>
    /// DTO za potvrdu komisije
    /// </summary>
    public class CommissionConfirmationDto
    {
        public Guid CommissionID { get; set; }
        public Member President { get; set; }
        public List<Member> MembersOfCommission { get; set; }
    }
}
