using AutoMapper;
using CommissionMicroservice.Data;
using CommissionMicroservice.Entities;
using CommissionMicroservice.Models;
using CommissionMicroservice.ServiceCalls;
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
    /// <summary>
    /// Kontroler clana komisije sa CRUD
    /// </summary>
    [ApiController]
    [Route("api/members")]
    [Produces("application/json", "application/xml")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository memberRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerMicroservice loggerMicroservice;
        private readonly LoggerDto loggerDto;

        public MemberController(IMemberRepository memberRepository, IMapper mapper, LinkGenerator linkGenerator, ILoggerMicroservice loggerMicroservice)
        {
            this.memberRepository = memberRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.loggerMicroservice = loggerMicroservice;
            loggerDto = new LoggerDto();
            loggerDto.Service = "Member";
        }

        /// <summary>
        /// Vraca clanove komisije po zadatom imenu i prezimenu
        /// </summary>
        /// <param name="firstName">Ime clana komisije</param>
        /// <param name="lastName">Prezime clana komisije</param>
        /// <returns></returns>
        /// <response code="200">Vraća listu clanova komisije</response>
        /// <response code="204">Nema podataka</response>
        /// <response code="404">Nije pronađen ni jedan clan</response>
        [HttpGet]
        [HttpHead]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<MemberDto>> GetMembers(string firstName, string lastName)
        {
            loggerDto.HttpMethodName = "get";
            loggerDto.Date = " ";
            loggerDto.Time = " ";
            List<Member> members = memberRepository.GetMembers(firstName, lastName);
            if (members.Count == 0)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<List<MemberDto>>(members));
        }

        /// <summary>
        /// Vraca jednog clana sa proslednjenim ID-jem
        /// </summary>
        /// <param name="memberID">ID clana komisije</param>
        /// <returns>Vraca jednog clana</returns>
        /// <response code="200">Vraća clana komisije sa prosledjenim ID-jem</response>
        /// <response code="404">Nije pronađen ni jedan clan komisije sa tim ID-jem</response>
        /// <response code="500">Serverska greska</response>
        [HttpGet("{memberID}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MemberDto> GetMemberById(Guid memberID)
        {
            loggerDto.HttpMethodName = "GET";
            Member member = memberRepository.GetMemberById(memberID);
            if (member == null)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NotFound();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<MemberDto>(member));
        }

        /// <summary>
        /// Kreira novog clana komisije
        /// </summary>
        /// <param name="memberDto">Model clana komisije</param>
        /// <returns>Potvrda o kreiranom clanu komisije</returns>
        /// <response code="201">Clan komisije je uspešno kreiran</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja clana komisije</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MemberConfirmationDto> CreateMember([FromBody] MemberCreationDto memberDto)
        {
            try
            {
                loggerDto.HttpMethodName = "POST";
                bool memberValid = ValidateMember(memberDto);

                if (!memberValid)
                {
                    return BadRequest("You need to fill all of the fields");
                }

                Member member = mapper.Map<Member>(memberDto);
                MemberConfirmation confirmation = memberRepository.CreateMember(member);
                string location = linkGenerator.GetPathByAction("GetMemberById", "Member", new { memberID = confirmation.MemberID });
                loggerDto.Response = "201 CREATED";
                loggerMicroservice.CreateLog(loggerDto);
                return Created(location, mapper.Map<MemberConfirmationDto>(confirmation));
            }
            catch(Exception ex)
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Brisanje clana komisije sa proslednjenim ID-jem
        /// </summary>
        /// <param name="memberID">ID clana komisije</param>
        /// <returns></returns>
        /// <response code="404">Nije pronađen clan komisije</response>
        /// <response code="204">Clan komisije sa prosleđenim id-jem je obrisan</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja clana komisije</response>
        [HttpDelete("{memberID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteMember(Guid memberID)
        {
            try
            {
                loggerDto.HttpMethodName = "DELETE";
                Member member = memberRepository.GetMemberById(memberID);
                if (member == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                memberRepository.DeleteMember(memberID);
                memberRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Modifikacija clana komisije
        /// </summary>
        /// <param name="memberDto">Model clana komisije</param>
        /// <returns>Potvrda o modifikovanom clanu komisije</returns>
        /// <response code="200">Vraća ažuriranog clana komisije</response>
        /// <response code="404">Clan komisije kog je potrebno ažurirati nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja clana komisije</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MemberUpdateDto> UpdateMember([FromBody] MemberUpdateDto memberDto)
        {
            try
            {
                loggerDto.HttpMethodName = "PUT";
                var oldMember = memberRepository.GetMemberById(memberDto.MemberID);
                if (oldMember == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }
                Member member = mapper.Map<Member>(memberDto);
                mapper.Map(member, oldMember);
                memberRepository.SaveChanges();
                loggerDto.Response = "200 OK";
                loggerMicroservice.CreateLog(loggerDto);
                return Ok(mapper.Map<MemberDto>(oldMember));
            }
            catch (Exception)
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa clanovima komisije u sistemu
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetMemberOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        /// <summary>
        /// Proveravanje validnosti unesenih podataka
        /// </summary>
        /// <param name="member">Model clana komisije</param>
        /// <returns></returns>
        private static bool ValidateMember(MemberCreationDto member)
        {
            if (member.FirstName != null && member.LastName != null && member.Role != null && member.CommissionID != null)
            {
                return true;
            }
            return false;
        }
    }
}
