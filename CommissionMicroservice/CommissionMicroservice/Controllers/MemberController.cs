using AutoMapper;
using CommissionMicroservice.Data;
using CommissionMicroservice.Entities;
using CommissionMicroservice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Controllers
{
    [ApiController]
    [Route("api/members")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    [Authorize] //Ovom kontroleru mogu da pristupaju samo autorizovani korisnici
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository memberRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        public MemberController(IMemberRepository memberRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.memberRepository = memberRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<MemberDto>> GetMembers(string firstName, string lastName, string role, Commission commissionID)
        {
            List<Member> members = memberRepository.GetMembers(firstName, lastName, role, commissionID);
            if (members.Count == 0 || members == null)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<MemberDto>>(members));
        }

        [HttpGet("{memberID}")]
        public ActionResult<MemberDto> GetMemberById(Guid memberID)
        {
            Member member = memberRepository.GetMemberById(memberID);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<MemberDto>(member));
        }

        [HttpPost]
        public ActionResult<MemberConfirmationDto> CreateMember([FromBody] MemberCreationDto memberDto)
        {
            try
            {
                bool memberValid = ValidateMember(memberDto);

                if (!memberValid)
                {
                    return BadRequest("You need to fill all of the fields");
                }

                Member member = mapper.Map<Member>(memberDto);
                MemberConfirmation confirmation = memberRepository.CreateMember(member);
                string location = linkGenerator.GetPathByAction("GetMemberById", "Member", new { memberID = confirmation.MemberID });
                return Created(location, mapper.Map<MemberConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{memberID}")]
        public IActionResult DeleteMember(Guid memberID)
        {
            try
            {
                Member member = memberRepository.GetMemberById(memberID);
                if (member == null)
                {
                    return NotFound();
                }
                memberRepository.DeleteMember(memberID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        public ActionResult<MemberUpdateDto> UpdateMember([FromBody] MemberUpdateDto memberDto)
        {
            try
            {
                var oldMember = memberRepository.GetMemberById(memberDto.MemberID);
                if (oldMember == null)
                {
                    return NotFound();
                }
                Member member = mapper.Map<Member>(memberDto);
                mapper.Map(member, oldMember);
                return Ok(mapper.Map<MemberDto>(oldMember));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }

        [HttpOptions]
        public IActionResult GetMemberOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        private bool ValidateMember(MemberCreationDto member)
        {
            if (member.FirstName != null & member.LastName != null && member.Role != null && member.CommissionID != null)
            {
                return true;
            }
            return false;
        }
    }
}
