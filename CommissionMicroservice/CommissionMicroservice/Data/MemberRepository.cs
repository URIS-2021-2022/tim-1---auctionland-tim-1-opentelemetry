using AutoMapper;
using CommissionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Data
{
    public class MemberRepository : IMemberRepository
    {
        private readonly CommissionContext context;
        private readonly IMapper mapper;

        public MemberRepository(IMapper mapper, CommissionContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public MemberConfirmation CreateMember(Member member)
        {
            var createdEntity = context.Add(member);
            return mapper.Map<MemberConfirmation>(createdEntity.Entity);
        }

        public void DeleteMember(Guid memberID)
        {
            var member = GetMemberById(memberID);
            context.Remove(member);
        }

        public Member GetMemberById(Guid memberID)
        {
            return context.Members.FirstOrDefault(e => e.MemberID == memberID);
        }

        public List<Member> GetMembers(string firstName = null, string lastName = null, string role = null, Commission commission = null)
        {
            return context.Members.Where(m => (firstName == null || m.FirstName == firstName &&
                                               lastName == null || m.LastName == lastName &&
                                               role == null || m.Role == role)).ToList();
        }

        public void UpdateMember(Member member)
        {
            // ne mora nista
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
