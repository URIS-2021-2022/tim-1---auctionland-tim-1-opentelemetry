﻿using Microsoft.AspNetCore.Authorization;
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
    [Route("api/auctions")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    public class AuctionController : ControllerBase //Daje nam pristup korisnim poljima i metodama
    {
        private readonly IAuctionRepository auctionRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateAuction)
        private readonly IMapper mapper;
        private readonly ILoggerMicroservice loggerMicroservice;
        private readonly LoggerDto loggerDto;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public AuctionController(IAuctionRepository auctionRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerMicroservice loggerMicroservice)
        {
            this.auctionRepository = auctionRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerMicroservice = loggerMicroservice;
            loggerDto = new LoggerDto();
            loggerDto.Service = "Auction";
        }

        /// <summary>
        /// Vraća sve licitacije.
        /// </summary>
        /// <returns>Lista licitacija</returns>
        /// <response code="200">Vraća listu licitacija</response>
        /// <response code="404">Nije pronađena ni jedna jedina licitacija</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<AuctionDto>> GetAuctions()
        {
            loggerDto.HttpMethodName = "get";
            loggerDto.Date = " ";
            loggerDto.Time = " ";
            var auctions = auctionRepository.GetAuctions();

            //Ukoliko nismo pronašli ni jednu licitaciju vratiti status 204 (NoContent)
            if (auctions == null || auctions.Count == 0)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto); 
                return NoContent();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            //Ukoliko smo našli neke licitacije vratiti status 200 i listu pronađenih licitacija (DTO objekti)
            return Ok(mapper.Map<List<AuctionDto>>(auctions));
        }

        /// <summary>
        /// Vraća jednu licitaciju na osnovu ID-ja licitacije.
        /// </summary>
        /// <param name="auctionId">ID licitacije</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženu licitaciju</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Auction))] //Kada se koristi IActionResult
        [HttpGet("{auctionId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public ActionResult<AuctionDto> GetAuction(Guid auctionId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            loggerDto.HttpMethodName = "GET";
            var auction = auctionRepository.GetAuctionById(auctionId);

            if (auction == null)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NotFound();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<AuctionDto>(auction));
        }

        /// <summary>
        /// Kreira jednu licitaciju.
        /// </summary>
        /// <remarks>
        /// Primer zahteva za kreiranje licitacije \
        /// POST /api/auctions \
        /// {
        /// "number": 1,
        /// "year": 2021,
        /// "date": "2020-11-15T09:00:00",
        /// "priceIncrease": 10,
        /// "applicationDeadline": "2020-11-15T09:00:00",
        /// "stageId": "1c7ea607-8ddb-493a-87fa-4bf5893e965b"
        /// }
        /// </remarks>
        /// <response code="200">Vraća kreiranu licitaciju</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranje licitacije</response>
        [HttpPost]
        [Consumes("application/json")]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AuctionConfirmationDto> CreateAuction([FromBody] AuctionCreationDto auction)
        {
            try
            {
                loggerDto.HttpMethodName = "POST";
                Auction auctionEntity = mapper.Map<Auction>(auction);
                AuctionConfirmation confirmation = auctionRepository.CreateAuction(auctionEntity);
                auctionRepository.SaveChanges(); //Perzistiramo promene

                //Generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetAuction", "Auction", new { auctionId = confirmation.AuctionId });
                loggerDto.Response = "201 CREATED";
                loggerMicroservice.CreateLog(loggerDto);
                //Vratiti status 201 (Created), zajedno sa identifikatorom novokreiranog resursa (licitacije) i samom licitacijom.
                return Created(location, mapper.Map<AuctionConfirmationDto>(confirmation));
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
        /// Ažurira jednu licitaciju.
        /// </summary>
        /// <param name="auction">Model prijave licitacije koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj licitaciji.</returns>
        /// <response code="200">Vraća ažuriranu licitaciju</response>
        /// <response code="400">Licitacija koja se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja licitacije</response>
        [HttpPut]
        [Consumes("application/json")]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AuctionDto> UpdateAuction(AuctionUpdateDto auction)
        {
            try
            {
                loggerDto.HttpMethodName = "PUT";
                //Proveriti da li uopšte postoji licitacija koju pokušavamo da ažuriramo.
                var oldAuction = auctionRepository.GetAuctionById(auction.AuctionId);
                if (oldAuction == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                Auction auctionEntity = mapper.Map<Auction>(auction);

                mapper.Map(auctionEntity, oldAuction); //Update objekta koji treba da sačuvamo u bazi                

                auctionRepository.SaveChanges(); //Perzistiramo promene
                loggerDto.Response = "200 OK";
                loggerMicroservice.CreateLog(loggerDto);
                return Ok(mapper.Map<AuctionDto>(oldAuction));
            }
            catch (Exception)
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jedne licitacije na osnovu ID-ja prijave.
        /// </summary>
        /// <param name="auctionId">ID licitacije </param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Licitacija uspešno obrisana</response>
        /// <response code="404">Nije pronađena licitacija za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja licitacija</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{auctionId}")]
        public IActionResult DeleteAuction(Guid auctionId)
        {
            try
            {
                loggerDto.HttpMethodName = "DELETE";
                var auction = auctionRepository.GetAuctionById(auctionId);

                if (auction == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                auctionRepository.DeleteAuction(auctionId);
                auctionRepository.SaveChanges();
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
        /// Vraća opcije za rad sa licitacijama.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        public IActionResult GetAuctionOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
