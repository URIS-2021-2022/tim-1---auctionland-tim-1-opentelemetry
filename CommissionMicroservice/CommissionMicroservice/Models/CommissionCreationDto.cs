using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Models
{
    /// <summary>
    /// DTO za kreiranje komisije
    /// </summary>
    public class CommissionCreationDto : IValidatableObject
    {
        public Guid CommissionID { get; set; }
        public Member President { get; set; }
        public List<Member> MembersOfCommission { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (President == null)
            {
                yield return new ValidationResult(
                    "Mora biti dodeljen predsednik komisije!", 
                    new[] { "MemberCreationDto" });
            }
        }
    }
}
