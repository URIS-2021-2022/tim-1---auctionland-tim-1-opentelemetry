using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Data
{
    public interface IMemberRepository
    {
        List<Member> GetMembers(string firstName = null, string lastName = null, string role = null, Commission commission = null);
        Member GetMemberById(Guid memberID);
        MemberConfirmation CreateMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(Guid memberID);
        bool SaveChanges();
    }
}
