using AutoMapper;
using DocumentMsProject.Data;
using DocumentMsProject.Entities;
using DocumentMsProject.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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

        /// <summary>
        /// Vraća listu svih ugovora o zakupu u vezi sa javnim nadmetanjem
        /// </summary>
        /// <returns>Lista ugovora o zakupu u vezi sa javnim nadmetanjem</returns>
        /// <response code="200">Vraća listu ugovora</response>
        /// <response code="404">Nije pronađen ni jedan jedini ugovor</response>
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

        /// <summary>
        /// Vraća ugovor o zakupu u vezi sa javnim nadmetanjem na osnovu ID
        /// </summary>
        /// <param name="leaseId">ID ugovora</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženi ugovor</response>
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

        /// <summary>
        /// Kreira novi ugovor o zakupu.
        /// </summary>
        /// <param name="leaseCreation">Model ugovora o zakupu</param>
        /// <returns>Potvrdu o kreiranom ugovoru.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog dokumenta \
        /// POST /api/document/leaseAgreement \
        /// {     \
        ///     "leaseTypeOfGuarantee": "tip garanta", \
        ///     "leaseAgreementMaturities": ""2012-11-15T10:30:00" \", \
        ///     "leaseAgreementEntryDate": ""2020-11-15T10:30:00" \", \
        ///     "ministerID": "4563cf92-b8d0-4574-9b40-a725f884da36", \
        ///     "publicBiddingID": "4563cf92-b8d0-4574-9b40-a725f884da36", \
        ///     "personID": "4563cf92-b8d0-4574-9b40-a725f884da36", \
        ///     "deadlineForLandRestitution": ""2012-11-15T10:30:00" \", \
        ///     "placeOfSigning": ""mesto" \", \
        ///     "dateOfSigning": ""2012-11-15T10:30:00" \", \
        ///}
        /// </remarks>
        /// <response code="200">Vraća kreirani dokument</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja dokumenta</response>
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

                return Created(location, mapper.Map<LeaseAgreementConfirmationDto>(confirmation)); //Druga opcija za vraćanje kreiranog resursa i lokacije
            }
            catch (Exception ex)
            {
                //TODO: Logovati ex
                //Ukoliko nastane neka greška na servisu vratiti status 500 (InternalServerError).
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        /// <summary>
        /// Vrši brisanje jednog ugovora o zakupu na osnovu ID-ja ugovora.
        /// </summary>
        /// <param name="leaseId">ID ugovora</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Ugovor o zakupu uspešno obrisan</response>
        /// <response code="404">Nije pronađen ugovor o zakupu za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja ugovora o zakupu</response>
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

        /// <summary>
        /// Ažurira jedan ugovor o zakupu.
        /// </summary>
        /// <param name="updateDto">Model ugovora o zakupu koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom ugovoru.</returns>
        /// <response code="200">Vraća ažurirani ugovor o zakupu</response>
        /// <response code="400">Ugovor o zakupu koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja ugovora o zakupu</response>
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

        /// <summary>
        /// Vraća opcije za rad sa ugovorom o zakupu
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetLeaseAgreementOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
