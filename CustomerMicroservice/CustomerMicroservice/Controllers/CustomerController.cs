using AutoMapper;
using CustomerMicroservice.Data;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models;
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
    /// Kontroler za kupca sa CRUD
    /// </summary>
    [ApiController]
    [Route("api/customers")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.customerRepository = customerRepository;
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
        public ActionResult<List<CustomerDto>> GetCustomers()
        {
            List<Customer> customers = customerRepository.GetCustomers();
            if (customers.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<CustomerDto>>(customers));
        }

        /// <summary>
        /// Vraca jednog kupca sa prosledjenim ID-jem
        /// </summary>
        /// <param name="customerId">ID kupca</param>
        /// <returns>Vraca jednog kupca</returns>
        /// <response code="200">Vraća kupca sa prosledjenim ID-jem</response>
        /// <response code="404">Nije pronađen ni jedan kupac sa tim ID-jem</response>
        [HttpGet("{customerID}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CustomerDto>> GetCustomerById(Guid customerId)
        {
            Customer customer = customerRepository.GetCustomerById(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CustomerDto>(customer));
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
        public ActionResult<CustomerConfirmationDto> CreateCustomer([FromBody] CustomerCreationDto customerDto)
        {
            // komunikacija
            try
            {
                customerDto.CustomerID = Guid.NewGuid();
                Customer customer = mapper.Map<Customer>(customerDto);
                CustomerConfirmation confirmation = customerRepository.CreateCustomer(customer);
                customerRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetCustomerById", "Customer", new { customerID = confirmation.CustomerID });

                return Created(location, mapper.Map<CustomerConfirmationDto>(confirmation));
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
        [HttpDelete("{customerID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCustomer(Guid customerID)
        {
            try
            {
                Customer customer = customerRepository.GetCustomerById(customerID);
                if (customer == null)
                {
                    return NotFound();
                }
                customerRepository.DeleteCustomer(customerID);
                customerRepository.SaveChanges();
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
        public ActionResult<CustomerConfirmationDto> UpdateCustomer([FromBody] CustomerUpdateDto customerDto)
        {
            try
            {
                var oldCustomer = customerRepository.GetCustomerById(customerDto.CustomerID);
                if (oldCustomer == null)
                {
                    return NotFound();
                }
                Customer customer = mapper.Map<Customer>(customerDto);
                mapper.Map(customer, oldCustomer);
                return Ok(mapper.Map<CustomerConfirmationDto>(oldCustomer));
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
        public IActionResult GetCustomerOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
