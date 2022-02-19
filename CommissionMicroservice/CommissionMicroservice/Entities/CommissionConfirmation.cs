using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Entities
{
    public class CommissionConfirmation
    {
        public Guid CommissionID { get; set; }
        public Member President { get; set; }
        public List<Member> MembersOfCommission { get; set; }
    }
}
