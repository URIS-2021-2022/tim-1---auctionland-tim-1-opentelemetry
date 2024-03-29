﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using AutoMapper;
using PublicBiddingMicroservice.Data;
using PublicBiddingMicroservice.Models;
using PublicBiddingMicroservice.Entities;
using PublicBiddingMicroservice.ServiceCalls;

namespace PublicBiddingMicroservice.Controllers
{
    [ApiController]
    [Route("api/publicBiddings")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    public class PublicBiddingController : ControllerBase //Daje nam pristup korisnim poljima i metodama
    {
        private readonly IPublicBiddingRepository publicBiddingRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreatePublicBidding)
        private readonly IMapper mapper;
        private readonly IAddressService addressService;
        private readonly IParcelService parcelService;
        private readonly ILoggerMicroservice loggerMicroservice;
        private readonly LoggerDto loggerDto;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public PublicBiddingController(IPublicBiddingRepository publicBiddingRepository, LinkGenerator linkGenerator, IMapper mapper, IAddressService addressService, IParcelService parcelService, ILoggerMicroservice loggerMicroservice)
        {
            this.publicBiddingRepository = publicBiddingRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.addressService = addressService;
            this.parcelService = parcelService;
            this.loggerMicroservice = loggerMicroservice;
            loggerDto = new LoggerDto
            {
                Service = "PublicBidding"
            };
        }

        /// <summary>
        /// Vraća sva javna nadmetanja za zadate filtere.
        /// </summary>
        /// <param name="numberOfParticipants">Broj ucesnika</param>
        /// <returns>Lista javnih nademtanja</returns>
        /// <response code="200">Vraća listu javnih nadmetanja</response>
        /// <response code="404">Nije pronađena ni jedno jedino javno nadmetanje</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PublicBiddingDto>> GetPublicBiddings(int numberOfParticipants)
        {
            loggerDto.HttpMethodName = "GET";
            loggerDto.Date = " ";
            loggerDto.Time = " ";
            List<PublicBidding> publicBiddings = publicBiddingRepository.GetPublicBiddings(numberOfParticipants);

            //Ukoliko nismo pronašli ni jedno javno nadmetanje vratiti status 204 (NoContent)
            if (publicBiddings == null || publicBiddings.Count == 0)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }

            foreach (PublicBidding b in publicBiddings)
            {
                AddressDto address = addressService.GetAddress(b.AddressId).Result;
                ParcelDto parcel = parcelService.GetParcel(b.ParcelId).Result;
                b.Address = address;
                b.Parcel = parcel;
            }

            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);

            //Ukoliko smo našli neko javno nadmetanje vratiti status 200 i listu pronađenih javnih nadmetanja (DTO objekti)
            return Ok(mapper.Map<List<PublicBiddingDto>>(publicBiddings));
        }

        /// <summary>
        /// Vraća jedno javno nadmetnaje na osnovu ID-ja javnog nadmetanja.
        /// </summary>
        /// <param name="publicBiddingId">ID javnog nadmetanja</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženo javno nadmetanje</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicBidding))] //Kada se koristi IActionResult
        [HttpGet("{publicBiddingId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public ActionResult<PublicBiddingDto> GetPublicBidding(Guid publicBiddingId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            loggerDto.HttpMethodName = "GET";
            PublicBidding publicBidding = publicBiddingRepository.GetPublicBiddingById(publicBiddingId);

            if (publicBidding == null)
            {
                loggerDto.Response = "404 NOT FOUND";
                loggerMicroservice.CreateLog(loggerDto);
                return NotFound();
            }

            AddressDto address = addressService.GetAddress(publicBidding.AddressId).Result;
            ParcelDto parcel = parcelService.GetParcel(publicBidding.ParcelId).Result;
            publicBidding.Address = address;
            publicBidding.Parcel = parcel;

            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<PublicBiddingDto>(publicBidding));
        }


        /// <summary>
        /// Kreira jedno javno nadmetanje.
        /// </summary>
        /// <remarks>
        /// Primer zahteva za kreiranje novog javnog nadmetanja \
        /// POST /api/publicBiddings \
        /// {
        /// "date": "2020-11-15T09:00:00",
        /// "startTime": "2020-11-15T09:00:00",
        /// "endTime": "2020-11-15T12:00:00",
        /// "startingPricePerHe": 10,
        /// "exclude": true,
        /// "auctionedPrice": 139,
        /// "leasePeriod": 1,
        /// "numberOfParticipants": 12,
        /// "depositReplenishment": 13,
        /// "circle": 1,
        /// "statusId": "1c7ea607-8ddb-493a-87fa-4bf5893e965b",
        /// "stageId": "1c7ea607-8ddb-493a-87fa-4bf5893e965b",
        /// "publicBiddingTypeId": "1c7ea607-8ddb-493a-87fa-4bf5893e965b",
        /// "addressId": "e5fd4e92-1938-4bd8-fbb3-08d9f617ed32"
        /// "parcelId": "866f2352-771f-4405-a9b5-9878b0fbff0f"
        /// }
        /// </remarks>
        /// <response code="200">Vraća kreirano javno nadmetanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja javnog nadmetanja</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PublicBiddingConfirmationDto> CreatePublicBidding([FromBody] PublicBiddingCreationDto publicBidding)
        {
            try
            {
                loggerDto.HttpMethodName = "POST";
                PublicBidding publicBiddingEntity = mapper.Map<PublicBidding>(publicBidding);
                PublicBiddingConfirmation confirmation = publicBiddingRepository.CreatePublicBidding(publicBiddingEntity);
                publicBiddingRepository.SaveChanges(); //Perzistiramo promene

                //Generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetPublicBidding", "PublicBidding", new { publicBiddingId = confirmation.PublicBiddingId });

                loggerDto.Response = "201 CREATED";
                loggerMicroservice.CreateLog(loggerDto);

                //Vratiti status 201 (Created), zajedno sa identifikatorom novokreiranog resursa (javnog nadmetanja) i samim javnim nadmetanjem.
                return Created(location, mapper.Map<PublicBiddingConfirmationDto>(confirmation));
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
        /// Ažurira jedno javno nadmetanje.
        /// </summary>
        /// <param name="publicBidding">Model javnog nadmetanja koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom javnom nadmetanju.</returns>
        /// <response code="200">Vraća ažurirano javno nadmetanje</response>
        /// <response code="400">Javno nadmetanje koja se ažurira nije pronađeno</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja javnog nadmetanja</response>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PublicBiddingDto> UpdatePublicBidding(PublicBiddingUpdateDto publicBidding)
        {
            try
            {
                loggerDto.HttpMethodName = "PUT";
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                var oldPublicBidding = publicBiddingRepository.GetPublicBiddingById(publicBidding.PublicBiddingId);
                if (oldPublicBidding == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                PublicBidding publicBiddingEntity = mapper.Map<PublicBidding>(publicBidding);

                mapper.Map(publicBiddingEntity, oldPublicBidding); //Update objekta koji treba da sačuvamo u bazi                

                publicBiddingRepository.SaveChanges(); //Perzistiramo promene
                loggerDto.Response = "200 OK";
                loggerMicroservice.CreateLog(loggerDto);
                return Ok(mapper.Map<PublicBiddingDto>(oldPublicBidding));
            }
            catch (Exception)
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jednog javnog nadmetanja na osnovu ID-ja javnog nadmetanja.
        /// </summary>
        /// <param name="publicBiddingId">ID javnog nadmetanja</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Javno nadmetanje uspešno obrisano</response>
        /// <response code="404">Nije pronađenao javno nadmetanje za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja javnog nadmetanja</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{publicBiddingId}")]
        public IActionResult DeletePublicBidding(Guid publicBiddingId)
        {
            try
            {
                loggerDto.HttpMethodName = "DELETE";
                var bidding = publicBiddingRepository.GetPublicBiddingById(publicBiddingId);

                if (bidding == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }

                publicBiddingRepository.DeletePublicBidding(publicBiddingId);
                publicBiddingRepository.SaveChanges();
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
        /// Vraća opcije za rad sa javnim nadmetanjima.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        public IActionResult GetPublicBiddingOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
