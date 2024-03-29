﻿using AutoMapper;
using DocumentMsProject.Data;
using DocumentMsProject.Entities;
using DocumentMsProject.Models;
using DocumentMsProject.ServiceCalls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMsProject.Controllers
{
    [ApiController]
    [Route("api/document/listOfDocuments")]
    [Produces("application/json", "application/xml")]
    public class ListOfDocumentsController : ControllerBase
    {
        private readonly IListOfDocumentsRepository documentsRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerMicroservice loggerMicroservice;
        private readonly LoggerDto loggerDto;

        public ListOfDocumentsController(IListOfDocumentsRepository documentsRepository, IMapper mapper, LinkGenerator linkGenerator, ILoggerMicroservice loggerMicroservice)
        {
            this.documentsRepository = documentsRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.loggerMicroservice = loggerMicroservice;
            loggerDto = new LoggerDto();
            loggerDto.Service = "LIST OF DOCUMENTS";
        }

        /// <summary>
        /// Vraća listu svih lista dokumenata u vezi sa javnim nadmetanjem
        /// </summary>
        /// <returns>Lista listi dokumenata u vezi sa javnim nadmetanjem</returns>
        /// <response code="200">Vraća listu lista dokumenata</response>
        /// <response code="404">Nije pronađena ni jedna jedina lista dokumenata</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ListOfDocumentsDto>> GetAllList()
        {
            loggerDto.HttpMethodName = "GET";
            var lists = documentsRepository.GetAllListOfDocuments();

            if (lists == null || lists.Count == 0)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<List<ListOfDocumentsDto>>(lists));
        }

        /// <summary>
        /// Vraća listu dokument u vezi sa javnim nadmetanjem na osnovu ID
        /// </summary>
        /// <param name="listId">ID liste dokumenata</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženu listu dokumenata javnog nadmetanja</response>
        [HttpGet("{listId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ListOfDocumentsDto> GetListOfDocumentsById(Guid listId)
        {
            loggerDto.HttpMethodName = "GET";
            ListOfDocuments listOfDocuments = documentsRepository.GetListOfDocumentsById(listId);
            if (listOfDocuments == null)
            {
                loggerDto.Response = "404 NOT FOUND";
                loggerMicroservice.CreateLog(loggerDto);
                return NotFound();
            }

            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<ListOfDocumentsDto>(listOfDocuments));
        }

        /// <summary>
        /// Kreira novu listu dokumenata.
        /// </summary>
        /// <param name="listCreation">Model liste dokumenata</param>
        /// <returns>Potvrdu o kreiranoj listi dokumenata.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje novog dokumenta \
        /// POST /api/document/listOfDocuments \
        ///     "listCreationDate": ""2020-11-15T10:30:00" \" \
        ///}
        /// </remarks>
        /// <response code="200">Vraća kreiranu listu dokumenata</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja liste dokumenata</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ListOfDocumentsConfirmationDto> CreateListOfDocuments([FromBody] ListOfDocumentsCreationDto listCreation)
        {
            try
            {
                loggerDto.HttpMethodName = "POST";
                ListOfDocuments listEntity = mapper.Map<ListOfDocuments>(listCreation);
                ListOfDocumentsConfirmation confirmation = documentsRepository.CreateListOfDocuments(listEntity);
                documentsRepository.SaveChanges(); //Perzistiramo promene

                //Generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetListOfDocumentsById", "ListOfDocuments", new { listId = confirmation.ListOfDocumentsId });

                loggerDto.Response = "201 CREATED";
                loggerMicroservice.CreateLog(loggerDto);
                return Created(location, mapper.Map<ListOfDocumentsConfirmationDto>(confirmation)); //Druga opcija za vraćanje kreiranog resursa i lokacije
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
        /// Vrši brisanje jedne lisete dokumenata na osnovu ID-ja liste dokumenata.
        /// </summary>
        /// <param name="listId">ID liste dokumenata</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Lista dokumenata uspešno obrisana</response>
        /// <response code="404">Nije pronađena lista dokumenata za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja liste dokumenta</response>
        [HttpDelete("{listId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteListOfDocuments(Guid listId)
        {
            try
            {
                loggerDto.HttpMethodName = "DELETE";
                var listOfDocs = documentsRepository.GetListOfDocumentsById(listId);
                if (listOfDocs == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }
                documentsRepository.DeleteListOfDocuments(listId);
                documentsRepository.SaveChanges();
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch(Exception)
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Ažurira jednu listu dokumenata.
        /// </summary>
        /// <param name="updateDto">Model liste dokumenta koja se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj listi dokumenata.</returns>
        /// <response code="200">Vraća ažuriranu listu dokumenata</response>
        /// <response code="400">Lista dokumenata koja se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja liste dokumenta za javno nadmetanje</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ListOfDocumentsConfirmationDto> UpdateListOfDocuments(ListOfDocumentsUpdateDto updateDto)
        {
            try
            {
                loggerDto.HttpMethodName = "PUT";
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldList = documentsRepository.GetListOfDocumentsById(updateDto.ListOfDocumentsId);
                if (oldList == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                ListOfDocuments listEntity = mapper.Map<ListOfDocuments>(updateDto);

                mapper.Map(listEntity, oldList); //Update objekta koji treba da sačuvamo u bazi                

                documentsRepository.SaveChanges(); //Perzistiramo promene
                loggerDto.Response = "200 OK";
                loggerMicroservice.CreateLog(loggerDto);
                return Ok(mapper.Map<ListOfDocumentsDto>(oldList));
            }
            catch (Exception)
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vraća opcije za rad sa listama dokumenata
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetListOfDocumentsOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
