using AutoMapper;
using DocumentMsProject.Data;
using DocumentMsProject.Entities;
using DocumentMsProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Controllers
{
    [ApiController]
    [Route("api/document/leaseAgreement")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class LeaseAgreementController : ControllerBase
    {
        private readonly ILeaseAgreementRepository agreementRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public LeaseAgreementController(ILeaseAgreementRepository agreementRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.agreementRepository = agreementRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<LeaseAgreementCreationDto>> GetAllLeases()
        {
            var leases = agreementRepository.GetAllLeases();

            //Ukoliko nismo pronašli ni jednu prijavu vratiti status 204 (NoContent)
            if (leases == null || leases.Count == 0)
            {
                return NoContent();
            }

            //Ukoliko smo našli neke prijava vratiti status 200 i listu pronađenih prijava (DTO objekti)
            return Ok(mapper.Map<List<LeaseAgreementDto>>(leases));
        }

        [HttpGet("{leaseId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<LeaseAgreementDto> GetLeaseById(Guid leaseId)
        {
            LeaseAgreement lease = agreementRepository.GetLeaseById(leaseId);
            if (lease == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<LeaseAgreementDto>(lease));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LeaseAgreementConfirmationDto> CreateLease([FromBody] LeaseAgreementCreationDto leaseCreation)
        {
            try
            {
                LeaseAgreement leaseEntity = mapper.Map<LeaseAgreement>(leaseCreation);
                LeaseAgreementConfirmation confirmation = agreementRepository.CreateLeaseAgreement(leaseEntity);
                agreementRepository.SaveChanges(); //Perzistiramo promene

                //Generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetLeaseById", "LeaseAgreement", new { leaseId = confirmation.LeaseAgreementID });

                //return Ok();
                return CreatedAtRoute(location, mapper.Map<LeaseAgreementConfirmationDto>(confirmation)); //Druga opcija za vraćanje kreiranog resursa i lokacije
            }
            catch (Exception ex)
            {
                //TODO: Logovati ex
                //Ukoliko nastane neka greška na servisu vratiti status 500 (InternalServerError).
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [HttpDelete("{leaseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteLease(Guid leaseId)
        {
            try
            {
                var lease = agreementRepository.GetLeaseById(leaseId);
                if (lease == null)
                {
                    return NotFound();
                }
                agreementRepository.DeleteLease(leaseId);
                agreementRepository.SaveChanges();
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LeaseAgreementConfirmationDto> UpdateLease(LeaseAgreementUpdateDto updateDto)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldLease = agreementRepository.GetLeaseById(updateDto.LeaseAgreementID);
                if (oldLease == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                LeaseAgreement leaseEntity = mapper.Map<LeaseAgreement>(updateDto);

                mapper.Map(leaseEntity, oldLease); //Update objekta koji treba da sačuvamo u bazi                

                agreementRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<LeaseAgreementDto>(oldLease));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetExamRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
