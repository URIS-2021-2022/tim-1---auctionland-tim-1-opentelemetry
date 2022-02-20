using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using AddressMicroservice.Data;
using AddressMicroservice.Etities;
using AddressMicroservice.Models;
using System.Linq;
using System.Threading.Tasks;
using AddressMicroservice.ServiceCalls;
using Microsoft.AspNetCore.Authorization;

namespace AddressMicroservice.Controllers
{
    /// <summary>
    /// Kontroler mikroservisa adresa.
    /// </summary>
    [ApiController]
    [Route("api/addresses")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    //AUTORIZACIJA ne prolazi posle negerisanja tokena
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository addressRepository;
        private readonly LinkGenerator linkGenerator; 
        private readonly IMapper mapper;
        private readonly ILoggerMicroservice loggerMicroservice;
        private readonly LoggerDto loggerDto;

        /// <summary>
        /// Konstruktor putem kog se injektuju zavisnosti.
        /// </summary>
        /// <param name="addressRepository"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        /// <param name="loggerMicroservice"></param>
        public AddressController(IAddressRepository addressRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerMicroservice loggerMicroservice)
        {
            this.addressRepository = addressRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerMicroservice = loggerMicroservice;
            loggerDto = new LoggerDto();
            loggerDto.Service = "ADDRESS";

        }

        /// <summary>
        /// Vraća sve adrese.
        /// </summary>
        /// <returns>Lista svih adresa u sistemu.</returns>
        /// <response code="200"> Vraća listu adresa.</response>
        /// <response code="204"> Nije pronađena ni jedna adresa.</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<AddressDto>> GetAllAddresses()
        {
            loggerDto.HttpMethodName = "GET";
            loggerDto.Date = " ";
            loggerDto.Time = " ";
            var addresses = addressRepository.GetAllAddresses();
            if (addresses == null || addresses.Count == 0)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<List<AddressDto>>(addresses));
        }

        /// <summary>
        /// Vraća jednu adresu sa prosleđenim ID-jem.
        /// </summary>
        /// <param name="addressID">ID adrese čije podatke treba pronaći.</param>
        /// <returns></returns>
        /// <response code="200"> Vraća adresu sa prosleđenim ID-jem.</response>
        /// <response code="204"> Nije pronađena adresa sa prosleđenim ID-jem.</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{addressID}")]
        public ActionResult<AddressDto> GetAddressById(Guid addressID)
        {
            loggerDto.HttpMethodName = "GET";
            var address = addressRepository.GetAddressById(addressID);
            if (address == null)
            {
                loggerDto.Response = "404 NOT FOUND";
                loggerMicroservice.CreateLog(loggerDto);
                return NotFound();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<AddressDto>(address));
        }

        /// <summary>
        /// Kreira novu adresu.
        /// </summary>
        /// <param name="addressCreation">Model adrese.</param>
        /// <returns>Potvrdu o kreiranoj adresi.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove adrese \
        /// POST /api/addresses \
        /// {     \
        ///     "addressID": "2841defc-761e-40d8-b8a3-d3e58516dca7", \
        ///     "street": "Isidore Sekulić", \
        ///     "number": 25, \
        ///     "zipCode": "21203", \
        ///     "cityID": "4563cf92-b8d0-4574-9b40-a725f884da36" , \
        ///     "cityName": "Veternik", \
        ///     "countryID": "4563cf92-b8d0-4574-9b40-a725f884da36", \
        ///     "countryName": "Srbija" \
        ///}
        /// </remarks>
        /// <response code="200">Vraća kreiranu adresu.</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja adrese.</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AddressConfirmationDto> CreateAddress([FromBody] AddressCreationDto addressCreation)
        {
            try
            {
                loggerDto.HttpMethodName = "POST";
                Address addressEntity = mapper.Map<Address>(addressCreation);
                AddressConfirmation confirmation = addressRepository.CreateAddress(addressEntity);
                addressRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetAddress", "Address", new { addressID = confirmation.AddressID });
                loggerDto.Response = "201 CREATED";
                loggerMicroservice.CreateLog(loggerDto);
                return CreatedAtRoute(location, mapper.Map<AddressConfirmationDto>(confirmation));
            }
            catch
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        /// <summary>
        /// Briše adresu sa prosleđenim ID-jem.
        /// </summary>
        /// <param name="addressID">ID adrese koju je potrebno obrisati.</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Adresa je obrisana.</response>
        /// <response code="404">Adresa sa prosleđenim ID-jem nije pronađena.</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja adrese.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{addressID}")]
        public IActionResult DeleteAddress(Guid addressID)
        {
            try
            {
                loggerDto.HttpMethodName = "DELETE";
                var address = addressRepository.GetAddressById(addressID);

                if (address == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }

                addressRepository.DeleteAddress(addressID);
                addressRepository.SaveChanges();
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }
            catch (Exception)
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        /// <summary>
        /// Ažurira adresu sa prosleđenim ID-jem.
        /// </summary>
        /// <param name="address">Model adrese.</param>
        /// <returns>Potvrda ažuriranja adrese.</returns>
        /// <response code="200">Vraća ažuriranu adresu.</response>
        /// <response code="400">Nije pronađena adresa sa prosleđenim ID-jem.</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja adrese.</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AddressConfirmationDto> UpdateAddress(AddressUpdateDto address)
        {
            try
            {
                loggerDto.HttpMethodName = "PUT";
                var oldAddress = addressRepository.GetAddressById(address.AddressID);
                if (oldAddress == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound(); 
                }
                Address addressEntity = mapper.Map<Address>(address);

                mapper.Map(addressEntity, oldAddress); 

                addressRepository.SaveChanges();
                loggerDto.Response = "200 OK";
                loggerMicroservice.CreateLog(loggerDto);
                return Ok(mapper.Map<AddressDto>(oldAddress));
            }
            catch (Exception)
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraća dozvoljene operacije sa adresama.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetExamRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
