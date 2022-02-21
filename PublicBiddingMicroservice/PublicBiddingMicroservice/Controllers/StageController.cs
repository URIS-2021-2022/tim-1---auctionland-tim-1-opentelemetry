using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using AutoMapper;
using PublicBiddingMicroservice.Models;
using PublicBiddingMicroservice.Entities;
using PublicBiddingMicroservice.Data;
using PublicBiddingMicroservice.ServiceCalls;

namespace PublicBiddingMicroservice.Controllers
{
    [ApiController]
    [Route("api/stages")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    [Authorize] //Ovom kontroleru mogu da pristupaju samo autorizovani korisnici
    public class StageController : ControllerBase //Daje nam pristup korisnim poljima i metodama
    {
        private readonly IStageRepository stageRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateStage)
        private readonly IMapper mapper;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public StageController(IStageRepository stageRepository, LinkGenerator linkGenerator, IMapper mapper, IAddressService addressService)
        {
            this.stageRepository = stageRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve etape.
        /// </summary>
        /// <returns>Lista etapa</returns>
        /// <response code="200">Vraća listu etapa</response>
        /// <response code="404">Nije pronađena ni jedna jedina etapa</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<StageDto>> GetStages()
        {
            var stages = stageRepository.GetStages();

            //Ukoliko nismo pronašli ni jednu etapu vratiti status 204 (NoContent)
            if (stages == null || stages.Count == 0)
            {
                return NoContent();
            }

            //Ukoliko smo našli neke etape vratiti status 200 i listu pronađenih etapa (DTO objekti)
            return Ok(mapper.Map<List<StageDto>>(stages));
        }

        /// <summary>
        /// Vraća jednu etapu na osnovu ID-ja etape.
        /// </summary>
        /// <param name="stageId">ID etape</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženu etapu</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Stage))] //Kada se koristi IActionResult
        [HttpGet("{stageId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public ActionResult<StageDto> GetStage(Guid stageId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var stage = stageRepository.GetStageById(stageId);

            if (stage == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<StageDto>(stage));
        }

        /// <summary>
        /// Kreira jednu etapu.
        /// </summary>
        /// <response code="200">Vraća kreiranu etapu</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranje etape</response>
        [HttpPost]
        [Consumes("application/json")]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StageConfirmationDto> CreateStage([FromBody] StageCreationDto stage)
        {
            try
            {
                Stage stageEntity = mapper.Map<Stage>(stage);
                StageConfirmation confirmation = stageRepository.CreateStage(stageEntity);
                stageRepository.SaveChanges(); //Perzistiramo promene

                //Generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetStage", "Stage", new { stageId = confirmation.StageId });

                //Vratiti status 201 (Created), zajedno sa identifikatorom novokreiranog resursa (etape).
                return Created(location, mapper.Map<StageConfirmationDto>(confirmation));
                //return CreatedAtRoute(); //Druga opcija za vraćanje kreiranog resursa i lokacije
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Ukoliko nastane neka greška na servisu vratiti status 500 (InternalServerError).
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        /// <summary>
        /// Ažurira jednu etapu.
        /// </summary>
        /// <param name="stage">Model prijave etape koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj etapi.</returns>
        /// <response code="200">Vraća ažuriranu etapu</response>
        /// <response code="400">Etapa koja se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja etape</response>
        [HttpPut]
        [Consumes("application/json")]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StageDto> UpdateStage(StageUpdateDto stage)
        {
            try
            {
                //Proveriti da li uopšte postoji etapa koju pokušavamo da ažuriramo.
                var oldStage = stageRepository.GetStageById(stage.StageId);
                if (oldStage == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                Stage stageEntity = mapper.Map<Stage>(stage);

                mapper.Map(stageEntity, oldStage); //Update objekta koji treba da sačuvamo u bazi                

                stageRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<StageDto>(oldStage));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jedne etape na osnovu ID-ja etape.
        /// </summary>
        /// <param name="stageId">ID etape</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Etapa uspešno obrisana</response>
        /// <response code="404">Nije pronađena etapa za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja etape</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{stageId}")]
        public IActionResult DeleteStage(Guid stageId)
        {
            try
            {
                var stage = stageRepository.GetStageById(stageId);

                if (stage == null)
                {
                    return NotFound();
                }

                stageRepository.DeleteStage(stageId);
                stageRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
        
        /// <summary>
        /// Vraća opcije za rad sa etapama.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        public IActionResult GetStageOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
