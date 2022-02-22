using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using AutoMapper;
using PublicBiddingRegistrationMicroservice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PublicBiddingRegistrationMicroservice.Models;
using PublicBiddingRegistrationMicroservice.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using PublicBiddingRegistrationMicroservice.ServiceCalls;
using PublicBiddingRegistrationMicroservice.ServiceCalls.CustomerMicroservice;

namespace PublicBiddingRegistrationMicroservice.Controllers
{
    [ApiController]
    [Route("api/application")]
    [Produces("application/json", "application/xml")]
    [Authorize]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;
        private readonly ILoggerMicroservice loggerMicroservice;
        private readonly ICustomerMicroservice customerMicroservice;
        private readonly LoggerDto loggerDto;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public ApplicationController(IApplicationRepository applicationRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerMicroservice loggerMicroservice, ICustomerMicroservice customerMicroservice)
        {
            this.applicationRepository = applicationRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerMicroservice = loggerMicroservice;
            this.customerMicroservice = customerMicroservice;
            loggerDto = new LoggerDto();
            loggerDto.Service = "APPLICATION";
        }

        /// <summary>
        /// Vraća listu svih prijava za javno nadmetanje
        /// </summary>
        /// <returns>Lista prijava za javno nadmetanje</returns>
        /// <response code="200">Vraća listu prijava za javno nadmetanje</response>
        /// <response code="404">Nije pronađena ni jedna jedina prijava za javno nadmetanje</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ApplicationDto>> GetApplications()
        {
            loggerDto.HttpMethodName = "GET";
            var applications = applicationRepository.GetApplications();

            if (applications == null || applications.Count == 0)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }

            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return (mapper.Map<List<ApplicationDto>>(applications));
        }

        /// <summary>
        /// Vraća prijavu za javno nadmetanje na osnovu ID
        /// </summary>
        /// <param name="applicationId">ID prijave za javno nadmetanje</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženu prijavu za javno nadmetanje</response>
        [HttpGet("{applicationId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ApplicationDto> GetApplicationById(Guid applicationId)
        {
            loggerDto.HttpMethodName = "GET";
            ApplicationForPublicBidding applicationForPublicBidding = applicationRepository.GetApplicationById(applicationId);
            if (applicationForPublicBidding == null)
            {
                loggerDto.Response = "404 NOT FOUND";
                loggerMicroservice.CreateLog(loggerDto);
                return NotFound();
            }

            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<ApplicationDto>(applicationForPublicBidding));
        }

        /// <summary>
        /// Kreira novu prijavu za javno nadmetanje.
        /// </summary>
        /// <param name="applicationCreation">Model prijave za javno nadmetanje</param>
        /// <returns>Potvrdu o kreiranoj prijavi.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove prijave za javno nadmetanje \
        /// POST /api/application \
        /// {     \
        ///     "paymentId": "2841defc-761e-40d8-b8a3-d3e58516dca7", \
        ///     "buyerId": "4563cf92-b8d0-4574-9b40-a725f884da36", \
        ///}
        /// </remarks>
        /// <response code="200">Vraća kreiranu prijavu za javno nadmetanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom prijave za javno nadmetanje</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ApplicationConformationDto> CreateApplication([FromBody] ApplicationCreationDto applicationCreation)
        {
            try
            {
                loggerDto.HttpMethodName = "POST";
                ApplicationForPublicBidding applicationEntity = mapper.Map<ApplicationForPublicBidding>(applicationCreation);
                ApplicationConfirmation confirmation = applicationRepository.CreateApplication(applicationEntity);
                applicationRepository.SaveChanges(); //Perzistiramo promene

                //Generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetApplicationById", "ApplicationForPublicBidding", new { applicationId = confirmation.ApplicationId });

                loggerDto.Response = "201 CREATED";
                loggerMicroservice.CreateLog(loggerDto);
                //Vratiti status 201 (Created), zajedno sa identifikatorom novokreiranog resursa (prijave ispita) i samom prijavom ispita.
                return CreatedAtRoute(location, mapper.Map<ApplicationConformationDto>(confirmation)); //Druga opcija za vraćanje kreiranog resursa i lokacije
            }
            catch
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                //Ukoliko nastane neka greška na servisu vratiti status 500 (InternalServerError).
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        /// <summary>
        /// Vrši brisanje jedne prijave za javno nadmetanje na osnovu ID-ja prijave.
        /// </summary>
        /// <param name="applicationId">ID prijave za javno nadmetanje</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Prijava za javno nadmetanje uspešno obrisana</response>
        /// <response code="404">Nije pronađena prijava za javno nadmetanje za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja prijave za javno nadmetanje</response>
        [HttpDelete("{applicationId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteApplication(Guid applicationId)
        {
            try
            {
                loggerDto.HttpMethodName = "DELETE";
                var application = applicationRepository.GetApplicationById(applicationId);
                if (application == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }
                applicationRepository.DeleteAplication(applicationId);
                applicationRepository.SaveChanges();
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
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
        /// Ažurira jednu prijavu za javno nadmetanje.
        /// </summary>
        /// <param name="updateDto">Model prijave za javno nadmetanje koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj prijavi.</returns>
        /// <response code="200">Vraća ažuriranu prijavu za javno nadmetanje</response>
        /// <response code="400">Prijava za javno nadmetanje koja se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja prijave za javno nadmetanje</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ApplicationConformationDto> UpdateApplication(ApplicationUpdateDto updateDto)
        {
            try
            {
                loggerDto.HttpMethodName = "PUT";
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldApplication = applicationRepository.GetApplicationById(updateDto.ApplicationId);
                if (oldApplication == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                ApplicationForPublicBidding applicationEntity = mapper.Map<ApplicationForPublicBidding>(updateDto);

                mapper.Map(applicationEntity, oldApplication); //Update objekta koji treba da sačuvamo u bazi                

                applicationRepository.SaveChanges(); //Perzistiramo promene
                loggerDto.Response = "200 OK";
                loggerMicroservice.CreateLog(loggerDto);
                return Ok(mapper.Map<ApplicationDto>(oldApplication));
            }
            catch (Exception)
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraća opcije za rad sa prijavama za javno nadmetanje
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
