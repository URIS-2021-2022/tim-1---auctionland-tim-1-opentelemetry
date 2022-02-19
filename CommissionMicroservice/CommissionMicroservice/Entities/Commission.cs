using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Entities
{
    public class Commission
    {
        [Key]
        public Guid CommissionID { get; set; }
        public Member President { get; set; }
        public List<Member> MembersOfCommission { get; set; }
    }
}
