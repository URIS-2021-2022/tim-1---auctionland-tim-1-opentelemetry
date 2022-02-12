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

namespace AddressMicroservice.Controllers
{
    [ApiController]
    [Route("api/addresses")]
    [Produces("application/json", "application/xml")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository addressRepository;
        private readonly LinkGenerator linkGenerator; 
        private readonly IMapper mapper;

        public AddressController(IAddressRepository addressRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.addressRepository = addressRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca sve adrese.
        /// </summary>
        /// <returns>Lista svih adresa u sistemu.</returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<AddressDto>> GetAllAddresses()
        {
            var addresses = addressRepository.GetAllAddresses();
            if (addresses == null || addresses.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<AddressDto>>(addresses));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{addressID}")]
        public ActionResult<AddressDto> GetAddressById(Guid addressID)
        {
            var address = addressRepository.GetAddressById(addressID);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<AddressDto>(address));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AddressConfirmationDto> CreateAddress([FromBody] AddressCreationDto addressCreation)
        {
            try
            {
                Address addressEntity = mapper.Map<Address>(addressCreation);
                AddressConfirmation confirmation = addressRepository.CreateAddress(addressEntity);
                addressRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetAddress", "Address", new { addressID = confirmation.AddressID });

                return Created(location, mapper.Map<AddressConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{addressID}")]
        public IActionResult DeleteAddress(Guid addressID)
        {
            try
            {
                var address = addressRepository.GetAddressById(addressID);

                if (address == null)
                {
                    return NotFound();
                }

                addressRepository.DeleteAddress(addressID);
                addressRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AddressConfirmationDto> UpdateAddress(AddressUpdateDto address)
        {
            try
            {
                var oldAddress = addressRepository.GetAddressById(address.AddressID);
                if (oldAddress == null)
                {
                    return NotFound(); 
                }
                Address addressEntity = mapper.Map<Address>(address);

                mapper.Map(addressEntity, oldAddress); 

                addressRepository.SaveChanges();
                return Ok(mapper.Map<AddressDto>(oldAddress));
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
