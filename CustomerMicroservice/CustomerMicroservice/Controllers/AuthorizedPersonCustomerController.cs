using AutoMapper;
using CustomerMicroservice.Data;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models.AuthorizedPersonCustomer;
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
    /// Kontroler za ovlasceno lice kupca
    /// </summary>
    [ApiController]
    [Route("api/authorizedPersonCustomers")]
    [Produces("application/json", "application/xml")] 
    //[Authorize]
    public class AuthorizedPersonCustomerController : ControllerBase
    {
        private readonly IAuthorizedPersonCustomerRepository authorizedPersonCustomerRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public AuthorizedPersonCustomerController(IAuthorizedPersonCustomerRepository authorizedPersonCustomerRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.authorizedPersonCustomerRepository = authorizedPersonCustomerRepository;
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
        public ActionResult<List<AuthorizedPersonCustomerDto>> GetAuthorizedPersonCustomers()
        {
            List<AuthorizedPersonCustomer> authorizedPersonCustomers = authorizedPersonCustomerRepository.GetAuthorizedPersonCustomers();
            if (authorizedPersonCustomers.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<AuthorizedPersonCustomerDto>>(authorizedPersonCustomers));
        }

        /// <summary>
        /// Vraca jednog kupca sa prosledjenim ID-jem
        /// </summary>
        /// <param name="customerId">ID kupca</param>
        /// <returns>Vraca jednog kupca</returns>
        /// <response code="200">Vraća kupca sa prosledjenim ID-jem</response>
        /// <response code="404">Nije pronađen ni jedan kupac sa tim ID-jem</response>
        [HttpGet("{authorizedPersonCustomerID}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<AuthorizedPersonCustomerDto>> GetAuthorizedPersonCustomerById(Guid authorizedPersonCustomerId)
        {
            AuthorizedPersonCustomer authorizedPersonCustomer = authorizedPersonCustomerRepository.GetAuthorizedPersonCustomerById(authorizedPersonCustomerId);
            if (authorizedPersonCustomer == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<AuthorizedPersonCustomerDto>(authorizedPersonCustomer));
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
        public ActionResult<AuthorizedPersonCustomerConfirmationDto> CreateAuthorizedPersonCustomer([FromBody] AuthorizedPersonCustomerCreationDto authorizedPersonCustomerDto)
        {
            // komunikacija
            try
            {
                authorizedPersonCustomerDto.AuthorizedPersonCustomerID = Guid.NewGuid();
                AuthorizedPersonCustomer authorizedPersonCustomer = mapper.Map<AuthorizedPersonCustomer>(authorizedPersonCustomerDto);
                AuthorizedPersonCustomerConfirmation confirmation = authorizedPersonCustomerRepository.CreateAuthorizedPersonCustomer(authorizedPersonCustomer);
                authorizedPersonCustomerRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetAuthorizedPersonCustomerById", "AuthorizedPersonCustomer", new { authorizedPersonCustomerID = confirmation.AuthorizedPersonCustomerID });

                return Created(location, mapper.Map<AuthorizedPersonCustomerConfirmationDto>(confirmation));
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
        [HttpDelete("{authorizedPersonCustomerID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAuthorizedPersonCustomer(Guid authorizedPersonCustomerID)
        {
            try
            {
                AuthorizedPersonCustomer authorizedPersonCustomer = authorizedPersonCustomerRepository.GetAuthorizedPersonCustomerById(authorizedPersonCustomerID);
                if (authorizedPersonCustomer == null)
                {
                    return NotFound();
                }
                authorizedPersonCustomerRepository.DeleteAuthorizedPersonCustomer(authorizedPersonCustomerID);
                authorizedPersonCustomerRepository.SaveChanges();
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
        public ActionResult<AuthorizedPersonCustomerConfirmationDto> UpdateAuthorizedPersonCustomer([FromBody] AuthorizedPersonCustomerUpdateDto authorizedPersonCustomerDto)
        {
            try
            {
                var oldAuthorizedPersonCustomer = authorizedPersonCustomerRepository.GetAuthorizedPersonCustomerById(authorizedPersonCustomerDto.AuthorizedPersonCustomerID);
                if (oldAuthorizedPersonCustomer == null)
                {
                    return NotFound();
                }
                AuthorizedPersonCustomer authorizedPersonCustomer = mapper.Map<AuthorizedPersonCustomer>(authorizedPersonCustomerDto);
                mapper.Map(authorizedPersonCustomer, oldAuthorizedPersonCustomer);
                return Ok(mapper.Map<AuthorizedPersonCustomerConfirmationDto>(oldAuthorizedPersonCustomer));
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
        public IActionResult GetAuthorizedPersonCustomerOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
