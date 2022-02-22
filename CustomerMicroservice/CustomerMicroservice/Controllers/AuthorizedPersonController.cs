using AutoMapper;
using CustomerMicroservice.Data;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models.AuthorizedPerson;
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
    /// Kontroler za ovlasceno lice
    /// </summary>
    [ApiController]
    [Route("api/authorizedPeople")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class AuthorizedPersonController : ControllerBase
    {
        private readonly IAuthorizedPersonRepository authorizedPersonRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public AuthorizedPersonController(IAuthorizedPersonRepository authorizedPersonRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.authorizedPersonRepository = authorizedPersonRepository;
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
        public ActionResult<List<AuthorizedPersonDto>> GetAuthorizedPeople()
        {
            List<AuthorizedPerson> authorizedPeople = authorizedPersonRepository.GetAuthorizedPeople();
            if (authorizedPeople.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<AuthorizedPersonDto>>(authorizedPeople));
        }

        /// <summary>
        /// Vraca jednog kupca sa prosledjenim ID-jem
        /// </summary>
        /// <param name="customerId">ID kupca</param>
        /// <returns>Vraca jednog kupca</returns>
        /// <response code="200">Vraća kupca sa prosledjenim ID-jem</response>
        /// <response code="404">Nije pronađen ni jedan kupac sa tim ID-jem</response>
        [HttpGet("{authorizedPersonID}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<AuthorizedPersonDto>> GetAuthorizedPersonById(Guid authorizedPersonId)
        {
            AuthorizedPerson authorizedPerson = authorizedPersonRepository.GetAuthorizedPersonById(authorizedPersonId);
            if (authorizedPerson == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<AuthorizedPersonDto>(authorizedPerson));
        }

        /// <summary>
        /// Kreira novog kupca
        /// </summary>
        /// <param name="customerDto">Model kupca</param>
        /// <returns>Potvrda o kreiranom kupcu</returns>
        /// <response code="201">Kupac je uspešno kreiran</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja kupca</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AuthorizedPersonConfirmationDto> CreateAuthorizedPerson([FromBody] AuthorizedPersonCreationDto authorizedPersonDto)
        {
            // komunikacija
            try
            {
                authorizedPersonDto.AuthorizedPersonID = Guid.NewGuid();
                AuthorizedPerson authorizedPerson = mapper.Map<AuthorizedPerson>(authorizedPersonDto);
                AuthorizedPersonConfirmation confirmation = authorizedPersonRepository.CreateAuthorizedPerson(authorizedPerson);
                authorizedPersonRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetAuthorizedPersonById", "AuthorizedPerson", new { authorizedPersonID = confirmation.AuthorizedPersonID });

                return Created(location, mapper.Map<AuthorizedPersonConfirmationDto>(confirmation));
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
        [HttpDelete("{authorizedPersonID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAuthorizedPerson(Guid authorizedPersonID)
        {
            try
            {
                AuthorizedPerson authorizedPerson = authorizedPersonRepository.GetAuthorizedPersonById(authorizedPersonID);
                if (authorizedPerson == null)
                {
                    return NotFound();
                }
                authorizedPersonRepository.DeleteAuthorizedPerson(authorizedPersonID);
                authorizedPersonRepository.SaveChanges();
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
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AuthorizedPersonConfirmationDto> UpdateAuthorizedPerson([FromBody] AuthorizedPersonUpdateDto authorizedPersonDto)
        {
            try
            {
                var oldAuthorizedPerson = authorizedPersonRepository.GetAuthorizedPersonById(authorizedPersonDto.AuthorizedPersonID);
                if (oldAuthorizedPerson == null)
                {
                    return NotFound();
                }
                AuthorizedPerson authorizedPerson = mapper.Map<AuthorizedPerson>(authorizedPersonDto);
                mapper.Map(authorizedPerson, oldAuthorizedPerson);
                return Ok(mapper.Map<AuthorizedPersonConfirmationDto>(oldAuthorizedPerson));
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
        public IActionResult GetAuthorizedPersonOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
