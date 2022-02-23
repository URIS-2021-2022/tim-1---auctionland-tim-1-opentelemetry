using AddressMicroservice.Models;
using AddressMicroservice.ServiceCalls;
using AdMicroservice.Data;
using AdMicroservice.Entities;
using AdMicroservice.Models;
using AdMicroservice.ServiceCalls;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Controllers
{
    /// <summary>
    /// Kreirana klasa AdController
    /// </summary>
    [ApiController]
    [Route("api/ads")]
    [Produces("application/json", "application/xml")]
    //[Authorize]
    public class AdController : ControllerBase
    {
        private readonly IAdRepository adRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IPublicBiddingService biddingService;
        private readonly ILoggerMicroservice loggerMicroservice;
        private readonly LoggerDto loggerDto;

        /// <summary>
        /// Kreiran konstruktor za injektovanje zavisnosti
        /// </summary>
        /// <param name="adRepository"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        public AdController(IAdRepository adRepository, LinkGenerator linkGenerator, IMapper mapper, IPublicBiddingService biddingService, ILoggerMicroservice loggerMicroservice)
        {
            this.adRepository = adRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.biddingService = biddingService;
            this.loggerMicroservice = loggerMicroservice;
            loggerDto = new LoggerDto();
            loggerDto.Service = "AD";
        }


        /// <summary>
        /// Vraća sve oglase koji su kreirani.
        /// </summary>
        /// <param name="municipalityName">Naziv opštine</param>
        /// <returns>Lista kreiranih oglasa</returns>
        /// <response code="200">Vraća listu kreiranih oglasa</response>
        /// <response code="404">Nije pronađen ni jedan oglas</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<AdDto2>> GetAds(string municipalityName)
        {
            loggerDto.HttpMethodName = "GET";
            var ads = adRepository.GetAds(municipalityName);

            if (ads == null || ads.Count == 0)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<List<AdDto2>>(ads));
        }

        /// <summary>
        /// Vraća jedan oglas na osnovu ID-ja oglasa.
        /// </summary>
        /// <param name="adId">ID oglasa</param>
        /// <returns>Traženi oglas</returns>
        /// <response code="200">Vraća traženi oglas</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExamRegistration))] //Kada se koristi IActionResult
        [HttpGet("{adId}")]
        public ActionResult<AdDto> GetAd(Guid adId)
        {
            loggerDto.HttpMethodName = "GET";
            Ad model = adRepository.GetAdById(adId);
            PublicBiddingDto dto = biddingService.GetPublicBiddingById(Guid.Parse("6A411C13-A195-48F7-8DBD-67596C3974C0")).Result;

            if (model == null)
            {
                loggerDto.Response = "404 NOT FOUND";
                loggerMicroservice.CreateLog(loggerDto);
                return NotFound();
            }

            /* if (biddingService.GetPublicBiddingByIdAsync() != null)
             {
                 return ;
             }*/

            var adWithPB = mapper.Map<AdDto>(model);
            adWithPB.PublicBidding = dto;
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<AdDto>(adWithPB));
        }

        /// <summary>
        /// Kreira novi oglas.
        /// </summary>
        /// <param name="adModel">Model oglasa</param>
        /// <returns>Potvrda o kreiranom oglasu</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog oglasa \
        /// POST /api/ads \
        /// {     \
        ///     "AdID": "2841defc-761e-40d8-b8a3-d3e58516dca7", \
        ///     "AdName": "Oglas 1", \
        ///     "OfficialListID": "4563cf92-b8d0-4574-9b40-a725f884da36", \
        ///     "MunicipalityName": "Palić", \
        ///     "OfficialListNumber": "xx/2022", \
        ///     "DateOfIssue": "2022-04-11T12:00:00", \
        ///}
        /// </remarks>
        /// <response code="200">Vraća kreiran oglas</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja oglasa</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AdDto2> CreateAd([FromBody] AdCreationDto adModel)
        {
            try
            {
                loggerDto.HttpMethodName = "POST";
                Ad ad = mapper.Map<Ad>(adModel);

                AdConfirmation confirmation = adRepository.CreateAd(ad);
                string location = linkGenerator.GetPathByAction("GetAd", "Ad", new { adId = confirmation.AdID });
                adRepository.SaveChanges();
                loggerDto.Response = "201 CREATED";
                loggerMicroservice.CreateLog(loggerDto);
                return Created(location, mapper.Map<AdConfirmationDto>(confirmation));
            }
            catch
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Ažurira jedan oglas.
        /// </summary>
        /// <param name="adModel">Model oglasa koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom oglasu</returns>
        /// <response code="200">Vraća ažuriran oglas</response>
        /// <response code="400">Oglas koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja oglasa</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AdConfirmationDto> UpdateAd(AdUpdateDto adModel)
        {
            try
            {
                loggerDto.HttpMethodName = "PUT";
                var oldAd = adRepository.GetAdById(adModel.AdID);
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (oldAd == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                Ad adEntity = mapper.Map<Ad>(adModel);
                mapper.Map(adEntity, oldAd);
                adRepository.SaveChanges();
                loggerDto.Response = "200 OK";
                loggerMicroservice.CreateLog(loggerDto);
                return Ok(mapper.Map<AdConfirmationDto>(oldAd));
            }
            catch
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jedog oglasa na osnovu ID-ja oglasa.
        /// </summary>
        /// <param name="adId">ID oglasa</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Oglas je uspešno obrisan</response>
        /// <response code="404">Nije pronađen oglas za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja oglasa</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{adId}")]
        public IActionResult DeleteAd(Guid adId)
        {
            try
            {
                loggerDto.HttpMethodName = "DELETE";
                var model = adRepository.GetAdById(adId);
                if (model == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }
                adRepository.DeleteAd(adId);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                adRepository.SaveChanges();
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
        /// Vraća opcije za rad sa oglasima.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetAdsOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
