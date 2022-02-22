using AutoMapper;
using CustomerMicroservice.Data;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models;
using CustomerMicroservice.Models.AuthorizedPersonCustomer;
using CustomerMicroservice.ServiceCalls;
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
    [Authorize]
    public class AuthorizedPersonCustomerController : ControllerBase
    {
        private readonly IAuthorizedPersonCustomerRepository authorizedPersonCustomerRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerMicroservice loggerMicroservice;
        private readonly LoggerDto loggerDto;

        public AuthorizedPersonCustomerController(IAuthorizedPersonCustomerRepository authorizedPersonCustomerRepository, 
                                                    IMapper mapper, LinkGenerator linkGenerator, ILoggerMicroservice loggerMicroservice)
        {
            this.authorizedPersonCustomerRepository = authorizedPersonCustomerRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.loggerMicroservice = loggerMicroservice;
            loggerDto = new LoggerDto();
            loggerDto.Service = "AuthorizedPersonCustomer";
        }

        /// <summary>
        /// Vraca sva ovlascena lica kupca
        /// </summary>
        /// <returns>Lista ovlascenih lica kupaca</returns>
        /// <response code="200">Vraća listu ovlascenih lica kupaca</response>
        /// <response code="404">Nije pronađen ni jedno ovlasceno lice kupca</response>
        [HttpHead]
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<AuthorizedPersonCustomerDto>> GetAuthorizedPersonCustomers()
        {
            loggerDto.HttpMethodName = "GET";
            loggerDto.Date = " ";
            loggerDto.Time = " ";
            List<AuthorizedPersonCustomer> authorizedPersonCustomers = authorizedPersonCustomerRepository.GetAuthorizedPersonCustomers();
            if (authorizedPersonCustomers.Count == 0)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<List<AuthorizedPersonCustomerDto>>(authorizedPersonCustomers));
        }

        /// <summary>
        /// Vraca jedno ovlasceno lice kupca sa prosledjenim ID-jem
        /// </summary>
        /// <param name="authorizedPersonCustomerId">ID ovlascenog lica kupca</param>
        /// <returns>Vraca jednog kupca</returns>
        /// <response code="200">Vraća kupca sa prosledjenim ID-jem</response>
        /// <response code="404">Nije pronađen ni jedan kupac sa tim ID-jem</response>
        [HttpGet("{authorizedPersonCustomerID}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<AuthorizedPersonCustomerDto>> GetAuthorizedPersonCustomerById(Guid authorizedPersonCustomerId)
        {
            loggerDto.HttpMethodName = "GET";
            AuthorizedPersonCustomer authorizedPersonCustomer = authorizedPersonCustomerRepository.GetAuthorizedPersonCustomerById(authorizedPersonCustomerId);
            if (authorizedPersonCustomer == null)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<AuthorizedPersonCustomerDto>(authorizedPersonCustomer));
        }

        /// <summary>
        /// Kreira novo ovlasceno lice kupca
        /// </summary>
        /// <param name="authorizedPersonCustomerDto">Model ovlascenog lica kupca</param>
        /// <returns>Potvrda o kreiranom kupcu</returns>
        /// <response code="201">Kupac je uspešno kreiran</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja kupca</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AuthorizedPersonCustomerConfirmationDto> CreateAuthorizedPersonCustomer([FromBody] AuthorizedPersonCustomerCreationDto authorizedPersonCustomerDto)
        {
            try
            {
                loggerDto.HttpMethodName = "POST";
                authorizedPersonCustomerDto.AuthorizedPersonCustomerID = Guid.NewGuid();
                AuthorizedPersonCustomer authorizedPersonCustomer = mapper.Map<AuthorizedPersonCustomer>(authorizedPersonCustomerDto);
                AuthorizedPersonCustomerConfirmation confirmation = authorizedPersonCustomerRepository.CreateAuthorizedPersonCustomer(authorizedPersonCustomer);
                authorizedPersonCustomerRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetAuthorizedPersonCustomerById", "AuthorizedPersonCustomer", new { authorizedPersonCustomerID = confirmation.AuthorizedPersonCustomerID });
                loggerDto.Response = "201 CREATED";
                loggerMicroservice.CreateLog(loggerDto);
                return Created(location, mapper.Map<AuthorizedPersonCustomerConfirmationDto>(confirmation));
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
        /// Brisanje ovlascenog lica kupca sa prosledjenim ID-jem
        /// </summary>
        /// <param name="authorizedPersonCustomerID">ID ovlascenog lica kupca</param>
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
                loggerDto.HttpMethodName = "DELETE";
                AuthorizedPersonCustomer authorizedPersonCustomer = authorizedPersonCustomerRepository.GetAuthorizedPersonCustomerById(authorizedPersonCustomerID);
                if (authorizedPersonCustomer == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }
                authorizedPersonCustomerRepository.DeleteAuthorizedPersonCustomer(authorizedPersonCustomerID);
                authorizedPersonCustomerRepository.SaveChanges();
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
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
        /// Modifikacija postojeceg ovlascenog lica kupca
        /// </summary>
        /// <param name="authorizedPersonCustomerDto">Model ovlascenog lica kupca</param>
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
                loggerDto.HttpMethodName = "PUT";
                var oldAuthorizedPersonCustomer = authorizedPersonCustomerRepository.GetAuthorizedPersonCustomerById(authorizedPersonCustomerDto.AuthorizedPersonCustomerID);
                if (oldAuthorizedPersonCustomer == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }
                AuthorizedPersonCustomer authorizedPersonCustomer = mapper.Map<AuthorizedPersonCustomer>(authorizedPersonCustomerDto);
                mapper.Map(authorizedPersonCustomer, oldAuthorizedPersonCustomer);
                loggerDto.Response = "200 OK";
                loggerMicroservice.CreateLog(loggerDto);
                return Ok(mapper.Map<AuthorizedPersonCustomerConfirmationDto>(oldAuthorizedPersonCustomer));
            }
            catch
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa ovlascenim licima kupaca u sistemu
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
