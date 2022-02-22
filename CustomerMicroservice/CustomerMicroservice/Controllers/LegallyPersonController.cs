/*using AutoMapper;
using CustomerMicroservice.Data;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models.LegallyPerson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerMicroservice.Controllers
{
    /// <summary>
    /// Kontroler za pravno lice
    /// </summary>
    [ApiController]
    [Route("api/legallyPeople")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class LegallyPersonController : ControllerBase
    {
        private readonly ILegallyPersonRepository legallyPersonRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public LegallyPersonController(ILegallyPersonRepository legallyPersonRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.legallyPersonRepository = legallyPersonRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Vraca sve kupce
        /// </summary>
        /// <returns>Lista kupaca</returns>
        /// <response code="200">Vraća listu kupaca</response>
        /// <response code="404">Nije pronađen ni jedan kupac</response>
        [HttpHead]
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<LegallyPersonDto>> GetLegallyPeople()
        {
            List<LegallyPerson> legallyPeople = legallyPersonRepository.GetLegallyPeople();
            if (legallyPeople.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<LegallyPersonDto>>(legallyPeople));
        }

        /// <summary>
        /// Vraca jednog kupca sa prosledjenim ID-jem
        /// </summary>
        /// <param name="customerId">ID kupca</param>
        /// <returns>Vraca jednog kupca</returns>
        /// <response code="200">Vraća kupca sa prosledjenim ID-jem</response>
        /// <response code="404">Nije pronađen ni jedan kupac sa tim ID-jem</response>
        [HttpGet("{legallyPersonID}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<LegallyPersonDto>> GetLegallyPersonById(Guid legallyPersonId)
        {
            LegallyPerson legallyPerson = legallyPersonRepository.GetLegallyPersonById(legallyPersonId);
            if (legallyPerson == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<LegallyPersonDto>(legallyPerson));
        }

        /// <summary>
        /// Kreira novog kupca
        /// </summary>
        /// <param name="customerDto">Model kupca</param>
        /// <returns>Potvrda o kreiranom kupcu</returns>
        /// <response code="201">Kupac je uspešno kreiran</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja kupca</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LegallyPersonConfirmationDto> CreateLegallyPerson([FromBody] LegallyPersonCreationDto legallyPersonDto)
        {
            // komunikacija
            try
            {
                legallyPersonDto.CustomerID = Guid.NewGuid();
                LegallyPerson legallyPerson = mapper.Map<LegallyPerson>(legallyPersonDto);
                LegallyPersonConfirmation confirmation = legallyPersonRepository.CreateLegallyPerson(legallyPerson);
                legallyPersonRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetLegallyPersonById", "LegallyPerson", new { legallyPersonID = confirmation.CustomerID });

                return Created(location, mapper.Map<LegallyPersonConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Brisanje kupca sa prosledjenim ID-jem
        /// </summary>
        /// <param name="customerID">ID kupca</param>
        /// <returns></returns>
        /// <response code="404">Nije pronađen kupac</response>
        /// <response code="204">Kupac sa prosleđenim id-jem je obrisan</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja kupca</response>
        [HttpDelete("{legallyPersonID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteLegallyPerson(Guid legallyPersonID)
        {
            try
            {
                LegallyPerson legallyPerson = legallyPersonRepository.GetLegallyPersonById(legallyPersonID);
                if (legallyPerson == null)
                {
                    return NotFound();
                }
                legallyPersonRepository.DeleteLegallyPerson(legallyPersonID);
                legallyPersonRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Modifikacija postojeceg kupca
        /// </summary>
        /// <param name="customerDto">Model kupca</param>
        /// <returns>Potvrda o modifikovanom kupcu</returns>
        /// <response code="200">Vraća ažuriranog kupca</response>
        /// <response code="404">Kupac kojeg je potrebno ažurirati nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja kupca</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LegallyPersonConfirmationDto> UpdateLegallyPerson([FromBody] LegallyPersonUpdateDto legallyPersonDto)
        {
            try
            {
                var oldLegallyPerson = legallyPersonRepository.GetLegallyPersonById(legallyPersonDto.CustomerID);
                if (oldLegallyPerson == null)
                {
                    return NotFound();
                }
                LegallyPerson legallyPerson = mapper.Map<LegallyPerson>(legallyPersonDto);
                mapper.Map(legallyPerson, oldLegallyPerson);
                return Ok(mapper.Map<LegallyPersonConfirmationDto>(oldLegallyPerson));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa kupcima u sistemu
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetLegallyPersonOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}*/
