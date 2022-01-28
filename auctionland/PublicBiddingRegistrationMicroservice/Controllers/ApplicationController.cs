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

namespace PublicBiddingRegistrationMicroservice.Controllers
{
    [ApiController]
    [Route("api/application")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public ApplicationController(IApplicationRepository applicationRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.applicationRepository = applicationRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<ApplicationDto>> GetApplications()
        {
            List<ApplicationForPublicBidding> applications = applicationRepository.GetApplications();
            if (applications == null || applications.Count == 0)
            {
                return NoContent();
            }
            return (mapper.Map<List<ApplicationDto>>(applications));
        }

        [HttpGet("{applicationId}")]
        public ActionResult<ApplicationDto> GetApplicationById(Guid applicationId)
        {
            ApplicationForPublicBidding applicationForPublicBidding = applicationRepository.GetApplicationById(applicationId);
            if (applicationForPublicBidding == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ApplicationDto>(applicationForPublicBidding));
        }

        [HttpPost]
        public ActionResult<ApplicationConformationDto> CreateApplication([FromBody] ApplicationCreationDto applicationCreation)
        {
            try
            {
                bool modelValid = ValidateApplication(applicationCreation);

                if (!modelValid)
                {
                    return BadRequest("Buyer and payment ids are same.");
                }

                ApplicationForPublicBidding application = mapper.Map<ApplicationForPublicBidding>(applicationCreation);

                ApplicationConfirmation confirmation = applicationRepository.CreateApplication(application);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetApplications", "ApplicationForPublicBidding", new { ApplicationId = confirmation.ApplicationId });
                return Created(location, mapper.Map<ApplicationConformationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{applicationId}")]
        public IActionResult DeleteApplication(Guid applicationId)
        {
            try
            {
                ApplicationForPublicBidding application = applicationRepository.GetApplicationById(applicationId);
                if (application == null)
                {
                    return NotFound();
                }
                applicationRepository.DeleteAplication(applicationId);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        public ActionResult<ApplicationConformationDto> UpdateExamRegistration(ApplicationUpdateDto updateDto)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (applicationRepository.GetApplicationById(updateDto.ApplicationId) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                ApplicationForPublicBidding applicationForPublicBidding = mapper.Map<ApplicationForPublicBidding>(updateDto);
                ApplicationConfirmation confirmation = applicationRepository.UpdateApplication(applicationForPublicBidding);
                return Ok(mapper.Map<ApplicationConformationDto>(confirmation));
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

        // Validira da student ne moze da prijavi ispit u visoj godini nego sto je prijavljen
        private bool ValidateApplication(ApplicationCreationDto applicationCreation)
        {
            if(applicationCreation.BuyerId == applicationCreation.PaymentId)
            {
                return false;
            }
            return true;
        }
    }
}
