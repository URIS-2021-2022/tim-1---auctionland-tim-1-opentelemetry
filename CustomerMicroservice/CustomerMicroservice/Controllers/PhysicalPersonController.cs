/*using AutoMapper;
using CustomerMicroservice.Data;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models.PhysicalPerson;
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
    /// Kontroler za fizicko lice
    /// </summary>
    [ApiController]
    [Route("api/physicalPeople")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class PhysicalPersonController : ControllerBase
    {
        private readonly IPhysicalPersonRepository physicalPersonRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public PhysicalPersonController(IPhysicalPersonRepository physicalPersonRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.physicalPersonRepository = physicalPersonRepository;
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
        public ActionResult<List<PhysicalPersonDto>> GetPhysicalPeople()
        {
            List<PhysicalPerson> physicalPeople = physicalPersonRepository.GetPhysicalPeople();
            if (physicalPeople.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<PhysicalPersonDto>>(physicalPeople));
        }

        /// <summary>
        /// Vraca jednog kupca sa prosledjenim ID-jem
        /// </summary>
        /// <param name="customerId">ID kupca</param>
        /// <returns>Vraca jednog kupca</returns>
        /// <response code="200">Vraća kupca sa prosledjenim ID-jem</response>
        /// <response code="404">Nije pronađen ni jedan kupac sa tim ID-jem</response>
        [HttpGet("{physicalPersonID}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<PhysicalPersonDto>> GetPhysicalPersonById(Guid physicalPersonId)
        {
            PhysicalPerson physicalPerson = physicalPersonRepository.GetPhysicalPersonById(physicalPersonId);
            if (physicalPerson == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PhysicalPersonDto>(physicalPerson));
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
        public ActionResult<PhysicalPersonConfirmationDto> CreatePhysicalPerson([FromBody] PhysicalPersonCreationDto physicalPersonDto)
        {
            // komunikacija
            try
            {
                physicalPersonDto.CustomerID = Guid.NewGuid();
                PhysicalPerson physicalPerson = mapper.Map<PhysicalPerson>(physicalPersonDto);
                PhysicalPersonConfirmation confirmation = physicalPersonRepository.CreatePhysicalPerson(physicalPerson);
                physicalPersonRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetPhysicalPersonById", "PhysicalPerson", new { physicalPersonID = confirmation.CustomerID });

                return Created(location, mapper.Map<PhysicalPersonConfirmationDto>(confirmation));
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
        [HttpDelete("{physicalPersonID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePhysicalPerson(Guid physicalPersonID)
        {
            try
            {
                PhysicalPerson physicalPerson = physicalPersonRepository.GetPhysicalPersonById(physicalPersonID);
                if (physicalPerson == null)
                {
                    return NotFound();
                }
                physicalPersonRepository.DeletePhysicalPerson(physicalPersonID);
                physicalPersonRepository.SaveChanges();
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
        public ActionResult<PhysicalPersonConfirmationDto> UpdatePhysicalPerson([FromBody] PhysicalPersonUpdateDto physicalPersonDto)
        {
            try
            {
                var oldPhysicalPerson = physicalPersonRepository.GetPhysicalPersonById(physicalPersonDto.CustomerID);
                if (oldPhysicalPerson == null)
                {
                    return NotFound();
                }
                PhysicalPerson physicalPerson = mapper.Map<PhysicalPerson>(physicalPersonDto);
                mapper.Map(physicalPerson, oldPhysicalPerson);
                return Ok(mapper.Map<PhysicalPersonConfirmationDto>(oldPhysicalPerson));
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
        public IActionResult GetPhysicalPersonOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}*/
