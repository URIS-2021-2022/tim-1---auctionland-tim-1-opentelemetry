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
    [Route("api/publicBiddingTypes")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    [Authorize] //Ovom kontroleru mogu da pristupaju samo autorizovani korisnici
    public class PublicBiddingTypeController : ControllerBase //Daje nam pristup korisnim poljima i metodama
    {
        private readonly IPublicBiddingTypeRepository publicBiddingTypeRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreatePublicBiddingType)
        private readonly IMapper mapper;
        private readonly ILoggerMicroservice loggerMicroservice;
        private readonly LoggerDto loggerDto;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public PublicBiddingTypeController(IPublicBiddingTypeRepository publicBiddingTypeRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerMicroservice loggerMicroservice)
        {
            this.publicBiddingTypeRepository = publicBiddingTypeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerMicroservice = loggerMicroservice;
            loggerDto = new LoggerDto();
            loggerDto.Service = "PublicBiddingType";
        }

        /// <summary>
        /// Vraća sve tipove javnog nadmetanja.
        /// </summary>
        /// <returns>Lista tipova javnog nadmetanja</returns>
        /// <response code="200">Vraća listu tipova javnog nadmetanja</response>
        /// <response code="404">Nije pronađena ni jedan jedini tip javnog nadmetanja</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PublicBiddingTypeDto>> GetPublicBiddingTypes()
        {
            loggerDto.HttpMethodName = "GET";
            loggerDto.Date = " ";
            loggerDto.Time = " ";
            var publicBiddingTypes = publicBiddingTypeRepository.GetPublicBiddingTypes();

            //Ukoliko nismo pronašli ni jedan tip javnog nadmetanja vratiti status 204 (NoContent)
            if (publicBiddingTypes == null || publicBiddingTypes.Count == 0)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            //Ukoliko smo našli neki tip javnog nadmetanja vratiti status 200 i listu pronađenih tipova javnog nadmetanja (DTO objekti)
            return Ok(mapper.Map<List<PublicBiddingTypeDto>>(publicBiddingTypes));
        }

        /// <summary>
        /// Vraća jedan tip javnog nadmetanja na osnovu ID-ja tipa.
        /// </summary>
        /// <param name="publicBiddingTypeId">ID tipa javnog nadmetanja</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženi tip javnog nadmetanja</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicBiddingType))] //Kada se koristi IActionResult
        [HttpGet("{publicBiddingTypeId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public ActionResult<PublicBiddingTypeDto> GetPublicBiddingType(Guid publicBiddingTypeId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            loggerDto.HttpMethodName = "GET";
            var publicBiddingType = publicBiddingTypeRepository.GetPublicBiddingTypeById(publicBiddingTypeId);

            if (publicBiddingType == null)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NotFound();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<PublicBiddingTypeDto>(publicBiddingType));
        }


        /// <summary>
        /// Kreira jedan tip javnog nadmetanja.
        /// </summary>
        /// <remarks>
        /// Primer zahteva za kreiranje novog tipa javnog nadmetanja \
        /// POST /api/publicBiddingTypes \
        /// {
        /// "publicBiddingTypesName": "Javna Licitacija"
        /// }
        /// </remarks>
        /// <response code="200">Vraća kreirani tip javnog nadmetanja</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja tipa javnog nadmetanja</response>
        [HttpPost]
        [Consumes("application/json")]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PublicBiddingTypeConfirmationDto> CreatePublicBiddingType([FromBody] PublicBiddingTypeCreationDto publicBiddingType)
        {
            try
            {
                loggerDto.HttpMethodName = "POST";
                PublicBiddingType publicBiddingTypeEntity = mapper.Map<PublicBiddingType>(publicBiddingType);
                PublicBiddingTypeConfirmation confirmation = publicBiddingTypeRepository.CreatePublicBiddingType(publicBiddingTypeEntity);
                publicBiddingTypeRepository.SaveChanges(); //Perzistiramo promene

                //Generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetPublicBiddingType", "PublicBiddingType", new { publicBiddingTypeId = confirmation.PublicBiddingTypeId });
                loggerDto.Response = "201 CREATED";
                loggerMicroservice.CreateLog(loggerDto);
                //Vratiti status 201 (Created), zajedno sa identifikatorom novokreiranog resursa (tipa javnog nadmetanja).
                return Created(location, mapper.Map<PublicBiddingTypeConfirmationDto>(confirmation));
                //return CreatedAtRoute(); //Druga opcija za vraćanje kreiranog resursa i lokacije
            }
            catch (Exception ex)
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                Console.WriteLine(ex);
                //Ukoliko nastane neka greška na servisu vratiti status 500 (InternalServerError).
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        /// <summary>
        /// Ažurira jedan tip javnog nadmetanja.
        /// </summary>
        /// <param name="publicBiddingType">Model prijave tipa javnog nadmetanja koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom tipu javnog nadmetanja.</returns>
        /// <response code="200">Vraća ažurirani tip javnog nadmetanja</response>
        /// <response code="400">Tip javnog nadmetanja koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja tipa javnog nadmetanja</response>
        [HttpPut]
        [Consumes("application/json")]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PublicBiddingTypeDto> UpdatePublicBiddingType(PublicBiddingTypeUpdateDto publicBiddingType)
        {
            try
            {
                loggerDto.HttpMethodName = "PUT";
                //Proveriti da li uopšte postoji tip javnog nadmetanja koju pokušavamo da ažuriramo.
                var oldPublicBiddingType = publicBiddingTypeRepository.GetPublicBiddingTypeById(publicBiddingType.PublicBiddingTypeId);
                if (oldPublicBiddingType == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                PublicBiddingType publicBiddingTypeEntity = mapper.Map<PublicBiddingType>(publicBiddingType);

                mapper.Map(publicBiddingTypeEntity, oldPublicBiddingType); //Update objekta koji treba da sačuvamo u bazi                

                publicBiddingTypeRepository.SaveChanges(); //Perzistiramo promene
                loggerDto.Response = "200 OK";
                loggerMicroservice.CreateLog(loggerDto);
                return Ok(mapper.Map<PublicBiddingTypeDto>(oldPublicBiddingType));
            }
            catch (Exception)
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jednog tipa javnog nadmetanja na osnovu ID-ja tipa.
        /// </summary>
        /// <param name="publicBiddingTypeId">ID tipa javnog nadmetanja</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Tip javnog nadmetanja je uspešno obrisan</response>
        /// <response code="404">Nije pronađen tip javnog nadmetanja za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja tipa javnog nadmetanja</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{publicBiddingTypeId}")]
        public IActionResult DeletePublicBiddingType(Guid publicBiddingTypeId)
        {
            try
            {
                loggerDto.HttpMethodName = "DELETE";
                var publicBiddingType = publicBiddingTypeRepository.GetPublicBiddingTypeById(publicBiddingTypeId);

                if (publicBiddingType == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }

                publicBiddingTypeRepository.DeletePublicBiddingType(publicBiddingTypeId);
                publicBiddingTypeRepository.SaveChanges();
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }
            catch (Exception)
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
        
        /// <summary>
        /// Vraća opcije za rad sa tipovima javnog nadmetanja.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        public IActionResult GetPublicBiddingTypeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
