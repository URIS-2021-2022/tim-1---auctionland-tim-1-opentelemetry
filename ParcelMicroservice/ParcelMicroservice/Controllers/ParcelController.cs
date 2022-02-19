using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ParcelMicroservice.Data;
using ParcelMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcelMicroservice.Controllers
{
    [ApiController]
    [Route("api/parcels")]
    public class ParcelController : ControllerBase
    {
        private readonly IParcelRepository parcelRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)

        public List<String> CultureList = new List<String> { "Njive", "Vrtovi", "Voćnjaci", "Vinogradi", "Livade", "Pašnjaci", "Šume", "Trstici-močvare" };
        public List<String> ClassList = new List<String> { "I", "II", "III", "IV", "V", "VI", "VII", "VIII" };
        public List<String> WorkabilityList = new List<String> { "Obradivo", "Ostalo" };
        public List<String> ProtectedZoneList = new List<String> { "1", "2", "3", "4" };
        public List<String> FormOfOwnershipList = new List<String> { "Privatna svojina", "Državna svojina RS", "Državna svojina", "Društvena svojina", "Zadružna svojina", "Mešovita svojina", "Drugi oblici" };
        public List<String> DrainageActualConditionList = new List<String> { "Površinsko odvodnjavanje", "Podzemno odvodnjavanje" };

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public ParcelController(IParcelRepository parcelRepository, LinkGenerator linkGenerator)
        {
            this.parcelRepository = parcelRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<ParcelModel>> GetParcels(string NumberOfParcel)
        {
            List<ParcelModel> parcels = parcelRepository.GetParcels(NumberOfParcel);
            if (parcels == null || parcels.Count == 0)
            {
                return NoContent();
            }
            return Ok(parcels);
        }

        [HttpGet("{parcelID}")]
        public ActionResult<ParcelModel> GetParcel(Guid parcelID)
        {
            ParcelModel model = parcelRepository.GetParcelById(parcelID);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        public ActionResult<ParcelConfirmation> CreateParcel([FromBody] ParcelModel model)
        {
            try
            {
                bool modelValid = ValidateParcel(model);

                if (!modelValid)
                {
                    return BadRequest("Parcela nema odgovarajuće vrednosti atributa... Molimo Vas da pokušate da izmenite vrednosti!");
                }

                ParcelConfirmation confirmation = parcelRepository.CreateParcel(model);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetParcels", "Parcel", new { parcelID = confirmation.ParcelID });
                return Created(location, confirmation);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpPut]
        public ActionResult<ParcelConfirmation> UpdateParcel([FromBody] ParcelModel model)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (parcelRepository.GetParcelById(model.ParcelID) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }

                ParcelConfirmation confirmation = parcelRepository.UpdateParcel(model);
                return Ok(confirmation);
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
                ParcelModel parcelModel = parcelRepository.GetParcelById(parcelID);
                if (parcelModel == null)
                {
                    return NotFound();
                }
                parcelRepository.DeleteParcel(parcelID);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        private bool ValidateParcel(ParcelModel model)
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
