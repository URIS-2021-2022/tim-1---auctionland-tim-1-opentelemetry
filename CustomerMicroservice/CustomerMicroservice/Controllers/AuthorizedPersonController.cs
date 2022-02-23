using AutoMapper;
using CustomerMicroservice.Data;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models;
using CustomerMicroservice.Models.AuthorizedPerson;
using CustomerMicroservice.Models.Exceptions;
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
    /// Kontroler za ovlasceno lice
    /// </summary>
    [ApiController]
    [Route("api/authorizedPeople")]
    [Produces("application/json", "application/xml")]
    public class AuthorizedPersonController : ControllerBase
    {
        private readonly IAuthorizedPersonRepository authorizedPersonRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly IAddressService addressService;
        private readonly ILoggerMicroservice loggerMicroservice;
        private readonly LoggerDto loggerDto;
        private readonly IPublicBiddingMicroservice publicBiddingMicroservice;

        public AuthorizedPersonController(IAuthorizedPersonRepository authorizedPersonRepository, IMapper mapper, LinkGenerator linkGenerator, 
                                            IAddressService addressService,IPublicBiddingMicroservice publicBiddingMicroservice, ILoggerMicroservice loggerMicroservice)
        {
            this.authorizedPersonRepository = authorizedPersonRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.addressService = addressService;
            this.publicBiddingMicroservice = publicBiddingMicroservice;
            this.loggerMicroservice = loggerMicroservice;
            loggerDto = new LoggerDto
            {
                Service = "AuthorizedPerson"
            };
        }

        /// <summary>
        /// Vraca sva ovlascena lica 
        /// </summary>
        /// <returns>Lista ovlascenih lica</returns>
        /// <response code="200">Vraća listu ovlascenih lica</response>
        /// <response code="404">Nije pronađeno ni jedno ovlasceno lice</response>
        [HttpHead]
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<AuthorizedPersonDto>> GetAuthorizedPeople()
        {
            loggerDto.HttpMethodName = "get";
            loggerDto.Date = " ";
            loggerDto.Time = " ";

            List<AuthorizedPerson> authorizedPeople = authorizedPersonRepository.GetAuthorizedPeople();
            if (authorizedPeople == null || authorizedPeople.Count == 0)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }

            foreach (AuthorizedPerson b in authorizedPeople)
            {
                AddressDto address = addressService.GetAddress(b.AddressId).Result;
                PublicBiddingDto publicBidding = publicBiddingMicroservice.GetPublicBiddings(b.PublicBiddingID).Result;
                b.Address = address;
                b.PublicBidding = publicBidding;
            }

            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<List<AuthorizedPersonDto>>(authorizedPeople));
        }

        /// <summary>
        /// Vraca jedno ovlasceno lice sa prosledjenim ID-jem
        /// </summary>
        /// <param name="authorizedPersonId">ID ovlascenog lica</param>
        /// <returns>Vraca jedno ovlasceno lice</returns>
        /// <response code="200">Vraća ovlasceno lice sa prosledjenim ID-jem</response>
        /// <response code="404">Nije pronađeno ni jedno ovlasceno lice sa tim ID-jem</response>
        [HttpGet("{authorizedPersonID}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<AuthorizedPersonDto>> GetAuthorizedPersonById(Guid authorizedPersonId)
        {
            loggerDto.HttpMethodName = "GET";
            AuthorizedPerson authorizedPerson = authorizedPersonRepository.GetAuthorizedPersonById(authorizedPersonId);
            if (authorizedPerson == null)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NotFound();
            }

            AddressDto address = addressService.GetAddress(authorizedPerson.AddressId).Result;
            PublicBiddingDto publicBidding = publicBiddingMicroservice.GetPublicBiddings(authorizedPerson.PublicBiddingID).Result;
            authorizedPerson.Address = address;
            authorizedPerson.PublicBidding = publicBidding;

            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<AuthorizedPersonDto>(authorizedPerson));
        }

        /// <summary>
        /// Kreira novo ovlasceno lice
        /// </summary>
        /// <param name="authorizedPersonDto">Model ovlascenog lica</param>
        /// <returns>Potvrda o kreiranom ovlascenom licu</returns>
        /// <response code="201">Ovlasceno lice je uspešno kreirano</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja ovlascenog lica</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AuthorizedPersonConfirmationDto> CreateAuthorizedPerson([FromBody] AuthorizedPersonCreationDto authorizedPersonDto)
        {
            try
            {
                loggerDto.HttpMethodName = "POST";
                authorizedPersonDto.AuthorizedPersonID = Guid.NewGuid();
                AuthorizedPerson authorizedPerson = mapper.Map<AuthorizedPerson>(authorizedPersonDto);
                AuthorizedPersonConfirmation confirmation = authorizedPersonRepository.CreateAuthorizedPerson(authorizedPerson);
                authorizedPersonRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetAuthorizedPersonById", "AuthorizedPerson", new { authorizedPersonID = confirmation.AuthorizedPersonID });

                loggerDto.Response = "201 CREATED";
                loggerMicroservice.CreateLog(loggerDto);

                return Created(location, mapper.Map<AuthorizedPersonConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Brisanje ovlascenog lica sa prosledjenim ID-jem
        /// </summary>
        /// <param name="authorizedPersonID">ID ovlascenog lica</param>
        /// <returns></returns>
        /// <response code="404">Nije pronađeno ovlasceno lice</response>
        /// <response code="204">Ovlasceno lice sa prosleđenim id-jem je obrisano</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja ovlascenog lica</response>
        [HttpDelete("{authorizedPersonID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteAuthorizedPerson(Guid authorizedPersonID)
        {
            try
            {
                loggerDto.HttpMethodName = "DELETE";
                AuthorizedPerson authorizedPerson = authorizedPersonRepository.GetAuthorizedPersonById(authorizedPersonID);
                if (authorizedPerson == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                authorizedPersonRepository.DeleteAuthorizedPerson(authorizedPersonID);
                authorizedPersonRepository.SaveChanges();
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
        /// Modifikacija postojeceg ovlascenog lica
        /// </summary>
        /// <param name="authorizedPersonDto">Model ovlascenog lica</param>
        /// <returns>Potvrda o modifikovanom ovlascenom licu</returns>
        /// <response code="200">Vraća ažurirano ovlasceno lice</response>
        /// <response code="404">Ovlasceno lice koje je potrebno ažurirati nije pronađeno</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja ovlascenog lica</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AuthorizedPersonConfirmationDto> UpdateAuthorizedPerson([FromBody] AuthorizedPersonUpdateDto authorizedPersonDto)
        {
            try
            {
                loggerDto.HttpMethodName = "PUT";
                var oldAuthorizedPerson = authorizedPersonRepository.GetAuthorizedPersonById(authorizedPersonDto.AuthorizedPersonID);
                if (oldAuthorizedPerson == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }
                AuthorizedPerson authorizedPerson = mapper.Map<AuthorizedPerson>(authorizedPersonDto);
                mapper.Map(authorizedPerson, oldAuthorizedPerson);
                authorizedPersonRepository.SaveChanges();
                loggerDto.Response = "200 OK";
                loggerMicroservice.CreateLog(loggerDto);
                return Ok(mapper.Map<AuthorizedPersonConfirmationDto>(oldAuthorizedPerson));
            }
            catch
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
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
