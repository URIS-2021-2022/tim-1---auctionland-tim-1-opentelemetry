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
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository addressRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;

        public AddressController(IAddressRepository addressRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.addressRepository = addressRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<AddressDto>> GetAllAddresses()
        {
            List<Address> addresses = addressRepository.GetAllAddresses();
            if (addresses == null || addresses.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<AddressDto>>(addresses));
        }

        [HttpGet("{addressID}")]
        public ActionResult<AddressDto> GetAddressById(Guid addressID)
        {
            Address addressModel = addressRepository.GetAddressById(addressID);
            if (addressModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<AddressDto>(addressModel));
        }

        [HttpPost]
        public ActionResult<AddressConfirmationDto> CreateAddress([FromBody] AddressCreationDto addressCreation)
        {
            try
            {
                Address address = mapper.Map<Address>(addressCreation);

                AddressConfirmation confirmation = addressRepository.CreateAddress(address);
                string location = linkGenerator.GetPathByAction("GetAddress", "Address", new { addressID = confirmation.AddressID });
                return Created(location, mapper.Map<AddressConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{addressID}")]
        public IActionResult DeleteAddress(Guid addressID)
        {
            try
            {
                Address addressModel = addressRepository.GetAddressById(addressID);
                if (addressModel == null)
                {
                    return NotFound();
                }
                addressRepository.DeleteAddress(addressID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        public ActionResult<AddressConfirmationDto> UpdateAddress(AddressUpdateDto address)
        {
            try
            {
                if (addressRepository.GetAddressById(address.AddressID) == null)
                {
                    return NotFound();
                }
                Address addressEntity = mapper.Map<Address>(address);
                AddressConfirmation confirmation = addressRepository.UpdateAddress(addressEntity);
                return Ok(mapper.Map<AddressConfirmationDto>(confirmation));
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
