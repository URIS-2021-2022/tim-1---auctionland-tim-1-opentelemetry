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
    [Route("api/parcels")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    //[Authorize] //Ovom kontroleru mogu da pristupaju samo autorizovani korisnici
    public class ParcelController : ControllerBase
    {
        private readonly IParcelRepository parcelRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;

        public List<String> CultureList = new List<String> { "Njive", "Vrtovi", "Voćnjaci", "Vinogradi", "Livade", "Pašnjaci", "Šume", "Trstici-močvare", "test" };
        public List<String> ClassList = new List<String> { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "test" };
        public List<String> WorkabilityList = new List<String> { "Obradivo", "Ostalo", "test" };
        public List<String> ProtectedZoneList = new List<String> { "1", "2", "3", "4", "test" };
        public List<String> FormOfOwnershipList = new List<String> { "Privatna svojina", "Državna svojina RS", "Državna svojina", "Društvena svojina", "Zadružna svojina", "Mešovita svojina", "Drugi oblici", "test" };
        public List<String> DrainageActualConditionList = new List<String> { "Površinsko odvodnjavanje", "Podzemno odvodnjavanje", "test" };

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public ParcelController(IParcelRepository parcelRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.parcelRepository = parcelRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<List<ParcelDto>> GetParcels(string NumberOfParcel)
        {
            List<Parcel> parcels = parcelRepository.GetParcels(NumberOfParcel);
            if (parcels == null || parcels.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ParcelDto>>(parcels));
        }

        [HttpGet("{parcelID}")]
        public ActionResult<ParcelDto> GetParcel(Guid parcelID)
        {
            Parcel model = parcelRepository.GetParcelById(parcelID);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ParcelDto>(model));
        }

        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<ParcelConfirmationDto> CreateParcel([FromBody] ParcelCreationDto model)
        {
            try
            {
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
                return Created(location, mapper.Map<ParcelConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [Consumes("application/json")]
        [HttpPut]
        public ActionResult<ParcelConfirmationDto> UpdateParcel(ParcelUpdateDto model)
        {
            try
            {
                var oldparcelEntity = parcelRepository.GetParcelById(model.ParcelID);
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (oldparcelEntity == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }

                Parcel parcelEntity = mapper.Map<Parcel>(model);

                mapper.Map(parcelEntity, oldparcelEntity); //Update objekta koji treba da sačuvamo u bazi                

                parcelRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<ParcelConfirmationDto>(oldparcelEntity));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpDelete("{parcelID}")]
        public IActionResult DeleteParcel(Guid parcelID)
        {
            try
            {
                Parcel parcelModel = parcelRepository.GetParcelById(parcelID);
                if (parcelModel == null)
                {
                    return NotFound();
                }
                parcelRepository.DeleteParcel(parcelID);
                parcelRepository.SaveChanges();
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
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
