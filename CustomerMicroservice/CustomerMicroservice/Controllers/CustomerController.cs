using AutoMapper;
using CustomerMicroservice.Data;
using CustomerMicroservice.Entities;
using CustomerMicroservice.Models;
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
    /// Kontroler za kupca sa CRUD
    /// </summary>
    [ApiController]
    [Route("api/customers")]
    [Produces("application/json", "application/xml")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly IPhysicalPersonRepository physicalPersonRepository;
        private readonly ILegallyPersonRepository legallyPersonRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly IAddressService addressService;
        private readonly IDocumentService documentService;
        private readonly ILoggerMicroservice loggerMicroservice;
        private readonly LoggerDto loggerDto;

        public CustomerController(IPhysicalPersonRepository physicalPersonRepository, ILegallyPersonRepository legallyPersonRepository,
                                    IMapper mapper, LinkGenerator linkGenerator, IAddressService addressService,
                                    IDocumentService documentService, ILoggerMicroservice loggerMicroservice)
        {
            this.physicalPersonRepository = physicalPersonRepository;
            this.legallyPersonRepository = legallyPersonRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.addressService = addressService;
            this.documentService = documentService;
            this.loggerMicroservice = loggerMicroservice;
            loggerDto = new LoggerDto();
            loggerDto.Service = "Customer";
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
            loggerDto.HttpMethodName = "GET";
            loggerDto.Date = " ";
            loggerDto.Time = " ";
            List<PhysicalPerson> physicalPeople = physicalPersonRepository.GetPhysicalPeople();
            List<LegallyPerson> legallyPeople = legallyPersonRepository.GetLegallyPeople();

            List<Customer> customers = physicalPeople.ConvertAll(p => (Customer)p);
            List<Customer> customersLegally = legallyPeople.ConvertAll(l => (Customer)l);

            customers.AddRange(customersLegally);
            if (customers == null || customers.Count == 0)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<CustomerDto>> GetCustomerById(Guid customerId)
        {
            loggerDto.HttpMethodName = "GET";
            Customer customer;
            customer = (Customer)physicalPersonRepository.GetPhysicalPersonById(customerId);

            if (customer == null)
            {
                customer = (Customer)legallyPersonRepository.GetLegallyPersonById(customerId);
            }

            if (customer == null)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
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
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CustomerConfirmationDto> CreateCustomer([FromBody] CustomerCreationDto customerDto)
        {
            // komunikacija
            try
            {
                loggerDto.HttpMethodName = "POST";
                customerDto.CustomerID = Guid.NewGuid();
                Customer customer = mapper.Map<Customer>(customerDto);
                CustomerConfirmation confirmation;

                if(customer.IsPhysicalPerson == true)
                {
                    PhysicalPerson physicalPerson = customer as PhysicalPerson;
                    confirmation = physicalPersonRepository.CreatePhysicalPerson(physicalPerson);
                    physicalPersonRepository.SaveChanges();
                }
                else
                {
                    LegallyPerson legallyPerson = customer as LegallyPerson;
                    confirmation = legallyPersonRepository.CreateLegallyPerson(legallyPerson);
                    legallyPersonRepository.SaveChanges();
                }

                string location = linkGenerator.GetPathByAction("GetCustomerById", "Customer", new { customerID = confirmation.CustomerID });
                loggerDto.Response = "201 CREATED";
                loggerMicroservice.CreateLog(loggerDto);
                return Created(location, mapper.Map<CustomerConfirmationDto>(confirmation));
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
            Customer customer;

            try
            {
                loggerDto.HttpMethodName = "DELETE";
                customer = legallyPersonRepository.GetLegallyPersonById(customerID);
                if (customer == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }

                if(customer.IsPhysicalPerson == true)
                {
                    physicalPersonRepository.DeletePhysicalPerson(customerID);
                    physicalPersonRepository.SaveChanges();
                }
                else if(customer.IsPhysicalPerson == false)
                {
                    legallyPersonRepository.DeleteLegallyPerson(customerID);
                    legallyPersonRepository.SaveChanges();
                }

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
        public ActionResult<CustomerConfirmationDto> UpdateCustomer([FromBody] CustomerUpdateDto customer)
        {
            try
            {
                loggerDto.HttpMethodName = "PUT";
                if (customer.IsPhysicalPerson == true)
                {
                    var oldCustomerPhy = physicalPersonRepository.GetPhysicalPersonById(customer.CustomerID);
                    oldCustomerPhy.CustomerID = customer.CustomerID;
                    oldCustomerPhy.IsPhysicalPerson = customer.IsPhysicalPerson;
                    oldCustomerPhy.Priority = customer.Priority;
                    oldCustomerPhy.RealizedArea = customer.RealizedArea;
                    oldCustomerPhy.HasABan = customer.HasABan;
                    oldCustomerPhy.StartDateBan = customer.StartDateBan;
                    oldCustomerPhy.DurationBan = customer.DurationBan;
                    oldCustomerPhy.EndDateBan = customer.EndDateBan;
                    oldCustomerPhy.AddressId = customer.AddressId;
                    oldCustomerPhy.DocumentID = customer.DocumentID;

                    if (oldCustomerPhy == null)
                    {
                        loggerDto.Response = "404 NOT FOUND";
                        loggerMicroservice.CreateLog(loggerDto);
                        return NotFound();
                    }

                    Customer customerEnt = mapper.Map<Customer>(customer);

                    PhysicalPerson physicalPerson = (PhysicalPerson)customerEnt;
                    mapper.Map(physicalPerson, oldCustomerPhy);
                    physicalPersonRepository.SaveChanges();
                    loggerDto.Response = "200 OK";
                    loggerMicroservice.CreateLog(loggerDto);
                    return Ok(mapper.Map<CustomerConfirmationDto>(oldCustomerPhy));
                }
                else
                {
                    var oldCustomerLeg = legallyPersonRepository.GetLegallyPersonById(customer.CustomerID);
                    oldCustomerLeg.CustomerID = customer.CustomerID;
                    oldCustomerLeg.IsPhysicalPerson = customer.IsPhysicalPerson;
                    oldCustomerLeg.Priority = customer.Priority;
                    oldCustomerLeg.RealizedArea = customer.RealizedArea;
                    oldCustomerLeg.HasABan = customer.HasABan;
                    oldCustomerLeg.StartDateBan = customer.StartDateBan;
                    oldCustomerLeg.DurationBan = customer.DurationBan;
                    oldCustomerLeg.EndDateBan = customer.EndDateBan;
                    oldCustomerLeg.AddressId = customer.AddressId;
                    oldCustomerLeg.DocumentID = customer.DocumentID;

                    if (oldCustomerLeg == null)
                    {
                        loggerDto.Response = "404 NOT FOUND";
                        loggerMicroservice.CreateLog(loggerDto);
                        return NotFound();
                    }

                    Customer customerEnt = mapper.Map<Customer>(customer);

                    LegallyPerson legallyPerson = (LegallyPerson)customerEnt;
                    mapper.Map(legallyPerson, oldCustomerLeg);
                    legallyPersonRepository.SaveChanges();
                    loggerDto.Response = "200 OK";
                    loggerMicroservice.CreateLog(loggerDto);
                    return Ok(mapper.Map<CustomerConfirmationDto>(oldCustomerLeg));
                }
                
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
        public IActionResult GetCustomerOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

    }
}
