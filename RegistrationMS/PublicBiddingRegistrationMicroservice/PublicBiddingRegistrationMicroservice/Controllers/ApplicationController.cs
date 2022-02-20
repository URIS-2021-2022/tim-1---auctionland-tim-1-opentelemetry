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
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ApplicationDto>> GetApplications()
        {
            var applications = applicationRepository.GetApplications();

            if (applications == null || applications.Count == 0)
            {
                return NoContent();
            }
            return (mapper.Map<List<ApplicationDto>>(applications));
        }

        [HttpGet("{applicationId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ApplicationConformationDto> CreateApplication([FromBody] ApplicationCreationDto applicationCreation)
        {
            try
            {
                ApplicationForPublicBidding applicationEntity = mapper.Map<ApplicationForPublicBidding>(applicationCreation);
                ApplicationConfirmation confirmation = applicationRepository.CreateApplication(applicationEntity);
                applicationRepository.SaveChanges(); //Perzistiramo promene

                //Generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetApplicationById", "ApplicationForPublicBidding", new { applicationId = confirmation.ApplicationId });

                //TODO: treba da se doda neka logika kao provera da li ce moci da se unese prijava ali sam je za sada preskocila

                //Vratiti status 201 (Created), zajedno sa identifikatorom novokreiranog resursa (prijave ispita) i samom prijavom ispita.
                return CreatedAtRoute(location, mapper.Map<ApplicationConformationDto>(confirmation)); //Druga opcija za vraćanje kreiranog resursa i lokacije
            }
            catch (Exception ex)
            {
                //TODO: Logovati ex
                //Ukoliko nastane neka greška na servisu vratiti status 500 (InternalServerError).
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [HttpDelete("{applicationId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteApplication(Guid applicationId)
        {
            try
            {
                var application = applicationRepository.GetApplicationById(applicationId);
                if (application == null)
                {
                    return NotFound();
                }
                applicationRepository.DeleteAplication(applicationId);
                applicationRepository.SaveChanges();
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ApplicationConformationDto> UpdateApplication(ApplicationUpdateDto updateDto)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldApplication = applicationRepository.GetApplicationById(updateDto.ApplicationId);
                if (oldApplication == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                ApplicationForPublicBidding applicationEntity = mapper.Map<ApplicationForPublicBidding>(updateDto);

                mapper.Map(applicationEntity, oldApplication); //Update objekta koji treba da sačuvamo u bazi                

                applicationRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<ApplicationDto>(oldApplication));
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
