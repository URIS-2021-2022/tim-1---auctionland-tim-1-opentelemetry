using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Models
{
    /// <summary>
    /// DTO za ažuriranje komisije
    /// </summary>
    public class CommissionUpdateDto : IValidatableObject
    {
        public Guid CommissionID { get; set; }
        public Member President { get; set; }
        public List<Member> MembersOfCommission { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
