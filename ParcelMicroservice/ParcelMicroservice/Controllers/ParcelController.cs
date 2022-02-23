using AddressMicroservice.ServiceCalls;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ParcelMicroservice.Data;
using ParcelMicroservice.Entities;
using ParcelMicroservice.Models;
using System;
using System.Collections.Generic;

namespace ParcelMicroservice.Controllers
{
    [ApiController]
    [Route("api/parcels")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    public class ParcelController : ControllerBase
    {
        private readonly IParcelRepository parcelRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;
        private readonly ILoggerMicroservice loggerMicroservice;
        private readonly LoggerDto loggerDto;

        private List<String> CultureList = new() { "Njive", "Vrtovi", "Voćnjaci", "Vinogradi", "Livade", "Pašnjaci", "Šume", "Trstici-močvare", "test" };
        private List<String> ClassList = new() { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "test" };
        private List<String> WorkabilityList = new() { "Obradivo", "Ostalo", "test" };
        private List<String> ProtectedZoneList = new() { "1", "2", "3", "4", "test" };
        private List<String> FormOfOwnershipList = new() { "Privatna svojina", "Državna svojina RS", "Državna svojina", "Društvena svojina", "Zadružna svojina", "Mešovita svojina", "Drugi oblici", "test" };
        private List<String> DrainageActualConditionList = new() { "Površinsko odvodnjavanje", "Podzemno odvodnjavanje", "test" };

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public ParcelController(IParcelRepository parcelRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerMicroservice loggerMicroservice)
        {
            this.parcelRepository = parcelRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerMicroservice = loggerMicroservice;
            loggerDto = new LoggerDto();
            loggerDto.Service = "PARCEL";
        }


        /// <summary>
        /// Vraća sve parcele za zadati filter.
        /// </summary>
        /// <param name="NumberOfParcel">Broj parcele</param>
        /// <returns>Listu parcela</returns>
        /// <response code="200">Vraća listu parcela</response>
        /// <response code="404">Nije pronađena ni jedna jedina parcela</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ParcelDto>> GetParcels(string NumberOfParcel)
        {
            loggerDto.HttpMethodName = "GET";
            loggerDto.Date = " ";
            loggerDto.Time = " ";
            List<Parcel> parcels = parcelRepository.GetParcels(NumberOfParcel);
            if (parcels == null || parcels.Count == 0)
            {
                loggerDto.Response = "204 NO CONTENT";
                loggerMicroservice.CreateLog(loggerDto);
                return NoContent();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<List<ParcelDto>>(parcels));
        }

        /// <summary>
        /// Vraća jednu parcelu na osnovu ID-ja parcele.
        /// </summary>
        /// <param name="parcelID">ID parceleparam>
        /// <returns></returns>
        /// <response code="200">Vraća traženu parcelu</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{parcelID}")]
        public ActionResult<ParcelDto> GetParcel(Guid parcelID)
        {
            loggerDto.HttpMethodName = "GET";
            Parcel model = parcelRepository.GetParcelById(parcelID);
            if (model == null)
            {
                loggerDto.Response = "404 NOT FOUND";
                loggerMicroservice.CreateLog(loggerDto);
                return NotFound();
            }
            loggerDto.Response = "200 OK";
            loggerMicroservice.CreateLog(loggerDto);
            return Ok(mapper.Map<ParcelDto>(model));
        }

        /// <summary>
        /// Kreira novu parcelu.
        /// </summary>
        /// <param name="model">Model parcele</param>
        /// <returns>Potvrdu o kreiranoj parceli.</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove parcele \
        /// POST /api/parcels \
        /// {     \
        ///SurfaceArea = 0, \
        ///NumberOfParcel = "xx/2022", \
        ///RealEstateListNumber = "0 - Prepis", \
        ///CultureRealStatus = "test", \
        ///ClassRealStatus = "test", \
        ///WorkabilityActualStatus = "test", \
        ///ProtectedZoneActualStatus = "test", \
        ///FormOfOwnership = "test", \
        ///DrainageActualCondition = "test", \
        ///MunicipalityID = Guid.Parse("2841defc-761e-40d8-b8a3-d3e58516dca7"), \
        ///NameOfTheMunicipality = "test" \
        ///}
        /// </remarks>
        /// <response code="200">Vraća kreiranu parcelu</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja parcele</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        [HttpPost]
        public ActionResult<ParcelConfirmationDto> CreateParcel([FromBody] ParcelCreationDto model)
        {
            try
            {
                loggerDto.HttpMethodName = "POST";
                bool modelValid = ValidateParcel(model);

                if (!modelValid)
                {
                    return BadRequest();
                }

                Parcel parcelEntity = mapper.Map<Parcel>(model); 

                ParcelConfirmation confirmation = parcelRepository.CreateParcel(parcelEntity);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetParcels", "Parcel", new { parcelID = confirmation.ParcelID });
                parcelRepository.SaveChanges();
                loggerDto.Response = "201 CREATED";
                loggerMicroservice.CreateLog(loggerDto);
                return Created(location, mapper.Map<ParcelConfirmationDto>(confirmation));
            }
            catch
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Ažurira jednu parcelu.
        /// </summary>
        /// <param name="model">Model parcele koja se ažurira</param>
        /// <returns>Potvrdu o modifikovanoj parceli.</returns>
        /// <response code="200">Vraća ažuriranu parcelu</response>
        /// <response code="400">Parcela koja se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja parcele</response>
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public ActionResult<ParcelConfirmationDto> UpdateParcel(ParcelUpdateDto model)
        {
            try
            {
                loggerDto.HttpMethodName = "PUT";
                var oldparcelEntity = parcelRepository.GetParcelById(model.ParcelID);
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (oldparcelEntity == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }

                Parcel parcelEntity = mapper.Map<Parcel>(model);

                mapper.Map(parcelEntity, oldparcelEntity); //Update objekta koji treba da sačuvamo u bazi                

                parcelRepository.SaveChanges(); //Perzistiramo promene
                loggerDto.Response = "200 OK";
                loggerMicroservice.CreateLog(loggerDto);
                return Ok(mapper.Map<ParcelConfirmationDto>(oldparcelEntity));
            }
            catch (Exception)
            {
                loggerDto.Response = "500 INTERNAL SERVER ERROR";
                loggerMicroservice.CreateLog(loggerDto);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jedne parcele na osnovu ID-ja parcele.
        /// </summary>
        /// <param name="parcelID">ID parcele</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Parcela je uspešno obrisana</response>
        /// <response code="404">Nije pronađena parcela za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja parcele</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{parcelID}")]
        public IActionResult DeleteParcel(Guid parcelID)
        {
            try
            {
                loggerDto.HttpMethodName = "DELETE";
                Parcel parcelModel = parcelRepository.GetParcelById(parcelID);
                if (parcelModel == null)
                {
                    loggerDto.Response = "404 NOT FOUND";
                    loggerMicroservice.CreateLog(loggerDto);
                    return NotFound();
                }
                parcelRepository.DeleteParcel(parcelID);
                parcelRepository.SaveChanges();
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
        /// Vraća opcije za rad sa parcelama
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetParcelsOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        private bool ValidateParcel(ParcelCreationDto model)
        {
            if (CultureList.Contains(model.CultureRealStatus))
            {
                return true;
            }
            else if (ClassList.Contains(model.ClassRealStatus))
            {
                return true;
            }
            else if (WorkabilityList.Contains(model.WorkabilityActualStatus))
            {
                return true;
            }
            else if (ProtectedZoneList.Contains(model.ProtectedZoneActualStatus))
            {
                return true;
            }
            else if (FormOfOwnershipList.Contains(model.FormOfOwnership))
            {
                return true;
            }
            else if (DrainageActualConditionList.Contains(model.DrainageActualCondition))
            {
                return true;
            }
            else
                return false;
        }
    }
}
