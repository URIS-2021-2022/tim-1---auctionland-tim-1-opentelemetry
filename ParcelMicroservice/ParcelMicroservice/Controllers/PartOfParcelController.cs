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
    [Route("api/partofparcels")]
    public class PartOfParcelController : ControllerBase
    {
        private readonly IPartOfParcelRepository partOfParcelRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)

        public PartOfParcelController(IPartOfParcelRepository partOfParcelRepository, LinkGenerator linkGenerator)
        {
            this.partOfParcelRepository = partOfParcelRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        public ActionResult<List<PartOfParcelModel>> GetPartOfParcels()
        {
            List<PartOfParcelModel> partOfParcels = partOfParcelRepository.GetPartOfParcels();
            if (partOfParcels == null || partOfParcels.Count == 0)
            {
                return NoContent();
            }
            return Ok(partOfParcels);
        }

        [HttpGet("{parcelID, partOfParcelID}")]
        public ActionResult<PartOfParcelModel> GetPartOfParcel(Guid parcelID, Guid partOfParcelID)
        {
            PartOfParcelModel model = partOfParcelRepository.GetPartOfParcelById(parcelID, partOfParcelID);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<PartOfParcelConfirmation> CreatePartOfParcel([FromBody] PartOfParcelModel model)
        {
            try
            {
                PartOfParcelConfirmation confirmation = partOfParcelRepository.CreatePartOfParcel(model);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetPartOfParcels", "PartOfParcel", new { parcelID = confirmation.ParcelID, partOfParcelID = confirmation.PartOfParcelID});//dodato
                return Created(location, confirmation);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [Consumes("application/json")]
        [HttpPut]
        public ActionResult<PartOfParcelConfirmation> UpdatePartOfParcel(PartOfParcelModel model)
        {
            try
            {
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (partOfParcelRepository.GetPartOfParcelById(model.ParcelID, model.PartOfParcelID) == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }

                PartOfParcelConfirmation confirmation = partOfParcelRepository.UpdatePartOfParcel(model);
                return Ok(confirmation);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpDelete("{parcelID, partOfParcelID}")]
        public IActionResult DeletePartOfParcel(Guid parcelID, Guid partOfParcelID)
        {
            try
            {
                PartOfParcelModel model = partOfParcelRepository.GetPartOfParcelById(parcelID, partOfParcelID);
                if (model == null)
                {
                    return NotFound();
                }
                partOfParcelRepository.DeletePartOfParcel(parcelID, partOfParcelID);
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


    }
}
