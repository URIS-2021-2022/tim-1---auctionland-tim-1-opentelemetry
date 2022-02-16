using AdMicroservice.Data;
using AdMicroservice.Entities;
using AdMicroservice.Models;
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
    [Authorize]
    public class AdController : ControllerBase
    {
        private readonly IAdRepository adRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        /// <summary>
        /// Kreiran konstruktor za injektovanje zavisnosti
        /// </summary>
        /// <param name="adRepository"></param>
        /// <param name="linkGenerator"></param>
        /// <param name="mapper"></param>
        public AdController(IAdRepository adRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.adRepository = adRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
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
        public ActionResult<List<AdDto>> GetAds(string municipalityName)
        {
            var ads = adRepository.GetAds(municipalityName);
            if (ads == null || ads.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<AdDto>>(ads));
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
            Ad model = adRepository.GetAdById(adId);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<AdDto>(model));
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
        public ActionResult<AdDto> CreateAd([FromBody] AdCreationDto adModel)
        {
            try
            {
                Ad ad = mapper.Map<Ad>(adModel);

                AdConfirmation confirmation = adRepository.CreateAd(ad);
                string location = linkGenerator.GetPathByAction("GetAd", "Ad", new { adId = confirmation.AdID });
                return Created(location, mapper.Map<AdConfirmationDto>(confirmation));
            }
            catch
            {
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
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (adRepository.GetAdById(adModel.AdID) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                Ad ad = mapper.Map<Ad>(adModel);
                AdConfirmation confirmation = adRepository.UpdateAd(ad);
                return Ok(mapper.Map<AdConfirmationDto>(confirmation));
            }
            catch (Exception)
            {
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
                Ad model = adRepository.GetAdById(adId);
                if (model == null)
                {
                    return NotFound();
                }
                adRepository.DeleteAd(adId);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Vraća opcije za rad sa oglasima
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
