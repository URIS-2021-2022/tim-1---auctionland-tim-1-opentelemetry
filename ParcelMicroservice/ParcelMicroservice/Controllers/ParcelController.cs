using AddressMicroservice.ServiceCalls;
using AutoMapper;
using LoggerMicroservice.Entities;
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
    [Route("api/parcels")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    //[Authorize] //Ovom kontroleru mogu da pristupaju samo autorizovani korisnici
    public class ParcelController : ControllerBase
    {
        private readonly IParcelRepository parcelRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;
        private readonly ILoggerMicroservice loggerMicroservice;
        private readonly LoggerDto loggerDto;

        public List<String> CultureList = new List<String> { "Njive", "Vrtovi", "Voćnjaci", "Vinogradi", "Livade", "Pašnjaci", "Šume", "Trstici-močvare", "test" };
        public List<String> ClassList = new List<String> { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "test" };
        public List<String> WorkabilityList = new List<String> { "Obradivo", "Ostalo", "test" };
        public List<String> ProtectedZoneList = new List<String> { "1", "2", "3", "4", "test" };
        public List<String> FormOfOwnershipList = new List<String> { "Privatna svojina", "Državna svojina RS", "Državna svojina", "Društvena svojina", "Zadružna svojina", "Mešovita svojina", "Drugi oblici", "test" };
        public List<String> DrainageActualConditionList = new List<String> { "Površinsko odvodnjavanje", "Podzemno odvodnjavanje", "test" };

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public ParcelController(IParcelRepository parcelRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerMicroservice loggerMicroservice)
        {
            this.parcelRepository = parcelRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerMicroservice = loggerMicroservice;
            loggerDto = new LoggerDto();
            loggerDto.Service = "ADDRESS";
        }

        [HttpGet]
        [HttpHead]
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

        [Consumes("application/json")]
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

        [Consumes("application/json")]
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
