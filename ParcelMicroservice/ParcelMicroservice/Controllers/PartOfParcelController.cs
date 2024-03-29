﻿using AddressMicroservice.ServiceCalls;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ParcelMicroservice.Data;
using ParcelMicroservice.Entities;
using ParcelMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Controllers
{
    [ApiController]
    [Route("api/parcels/partofparcels")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    public class PartOfParcelController : ControllerBase
    {
        private readonly IPartOfParcelRepository partOfParcelRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;
        private readonly ILoggerMicroservice loggerMicroservice;
        private readonly LoggerDto loggerDto;

        public PartOfParcelController(IPartOfParcelRepository partOfParcelRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerMicroservice loggerMicroservice)
        {
            this.partOfParcelRepository = partOfParcelRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerMicroservice = loggerMicroservice;
            loggerDto = new LoggerDto();
            loggerDto.Service = "PART OF PARCEL";
        }

        /// <summary>
        /// Vraća sve delove parcele.
        /// </summary>
        /// <returns>Lista delova parcela</returns>
        /// <response code="200">Vraća listu delova parcele</response>
        /// <response code="404">Nije pronađena ni jedan jedini deo parcele</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PartOfParcel>> GetPartOfParcels()
        {
            loggerDto.HttpMethodName = "GET";
            loggerDto.Date = " ";
            loggerDto.Time = " ";
            List<PartOfParcel> partOfParcels = partOfParcelRepository.GetPartOfParcels();
            if (partOfParcels == null || partOfParcels.Count == 0)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<List<PartOfParcelDto>>(partOfParcels));
        }

        /// <summary>
        /// Vraća jedan deo parcele na osnovu ID-ja dela parcele.
        /// </summary>
        /// <param name="partOfParcelID">ID dela parcele</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženi deo parcele</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{partOfParcelID}")]
        public ActionResult<PartOfParcel> GetPartOfParcel(Guid partOfParcelID)
        {
            loggerDto.HttpMethodName = "GET";
            PartOfParcel model = partOfParcelRepository.GetPartOfParcelById(partOfParcelID);
            if (model == null)
            {
                loggerDto.Response = "404 NOT FOUND";
                loggerMicroservice.CreateLog(loggerDto);
                return NotFound();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<PartOfParcelDto>(model));
        }

        /// <summary>
        /// Kreira novi deo parcele.
        /// </summary>
        /// <param name="model">Model dela parcele</param>
        /// <returns>Potvrdu o kreiranom delu parcele.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog dela parcele \
        /// POST /api/parcels/partofparcels \
        /// {    
        ///SurfaceAreaPOP = 0,\
        ///ClassID = Guid.Parse("61847780-396a-42a7-8e04-941e0d4eddf9"),\
        ///ClassLandLabel = "x"\
        ///}
        /// </remarks>
        /// <response code="200">Vraća kreiran deo parcele</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranje dela parcele</response>
        [Consumes("application/json")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PartOfParcelConfirmationDto> CreatePartOfParcel([FromBody] PartOfParcelCreationDto model)
        {
            try
            {
                loggerDto.HttpMethodName = "POST";
                PartOfParcel partOfParcelEntity = mapper.Map<PartOfParcel>(model);

                PartOfParcelConfirmation confirmation = partOfParcelRepository.CreatePartOfParcel(partOfParcelEntity);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetPartOfParcels", "PartOfParcel", new { partOfParcelID = confirmation.PartOfParcelID});//dodato
                partOfParcelRepository.SaveChanges();
                loggerDto.Response = "201 CREATED";
                loggerMicroservice.CreateLog(loggerDto);
                return Created(location, mapper.Map<PartOfParcelConfirmationDto>(confirmation));
            }
            catch
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Ažurira jedan deo parcele.
        /// </summary>
        /// <param name="model">Model dela parcele koja se ažurira</param>
        /// <returns>Potvrdu o modifikovanom delu parcele.</returns>
        /// <response code="200">Vraća ažuriran deo parcele</response>
        /// <response code="400">Deo parcele koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja dela parcele</response>
        [Consumes("application/json")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PartOfParcelConfirmationDto> UpdatePartOfParcel(PartOfParcelUpdateDto model)
        {
            try
            {
                loggerDto.HttpMethodName = "PUT";
                var oldPartOfParcelEntity = partOfParcelRepository.GetPartOfParcelById(model.PartOfParcelID);
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (oldPartOfParcelEntity == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }

                PartOfParcel partOfParcelEntity = mapper.Map<PartOfParcel>(model);

                mapper.Map(partOfParcelEntity, oldPartOfParcelEntity);

                partOfParcelRepository.SaveChanges();
                loggerDto.Response = "200 OK";
                loggerMicroservice.CreateLog(loggerDto);

                return Ok(mapper.Map<PartOfParcelConfirmationDto>(oldPartOfParcelEntity));
            }
            catch (Exception)
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jednog dela parcele na osnovu ID-ja dela parcele.
        /// </summary>
        /// <param name="partOfParcelID">ID dela parcele</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Deo parcele je uspešno obrisan</response>
        /// <response code="404">Nije pronađen deo parcele za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja dela parcele</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{partOfParcelID}")]
        public IActionResult DeletePartOfParcel(Guid partOfParcelID)
        {
            try
            {
                loggerDto.HttpMethodName = "DELETE";
                PartOfParcel model = partOfParcelRepository.GetPartOfParcelById(partOfParcelID);
                if (model == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }
                partOfParcelRepository.DeletePartOfParcel(partOfParcelID);
                partOfParcelRepository.SaveChanges();
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
        /// Vraća opcije za rad sa delovima parcele
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetPartOfParcelsOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}
