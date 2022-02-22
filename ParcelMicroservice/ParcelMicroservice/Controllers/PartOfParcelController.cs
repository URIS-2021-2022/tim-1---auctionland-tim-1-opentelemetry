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
    //[Authorize] //Ovom kontroleru mogu da pristupaju samo autorizovani korisnici
    public class PartOfParcelController : ControllerBase
    {
        private readonly IPartOfParcelRepository partOfParcelRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateExamRegistration)
        private readonly IMapper mapper;

        public PartOfParcelController(IPartOfParcelRepository partOfParcelRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.partOfParcelRepository = partOfParcelRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        [HttpHead]
        [HttpGet]
        public ActionResult<List<PartOfParcel>> GetPartOfParcels()
        {
            List<PartOfParcel> partOfParcels = partOfParcelRepository.GetPartOfParcels();
            if (partOfParcels == null || partOfParcels.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<PartOfParcelDto>>(partOfParcels));
        }

        [HttpGet("{partOfParcelID}")]
        public ActionResult<PartOfParcel> GetPartOfParcel(Guid partOfParcelID)
        {
            PartOfParcel model = partOfParcelRepository.GetPartOfParcelById(partOfParcelID);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PartOfParcelDto>(model));
        }

        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<PartOfParcelConfirmationDto> CreatePartOfParcel([FromBody] PartOfParcelCreationDto model)
        {
            try
            {
                PartOfParcel partOfParcelEntity = mapper.Map<PartOfParcel>(model);

                PartOfParcelConfirmation confirmation = partOfParcelRepository.CreatePartOfParcel(partOfParcelEntity);
                // Dobar API treba da vrati lokator gde se taj resurs nalazi
                string location = linkGenerator.GetPathByAction("GetPartOfParcels", "PartOfParcel", new { partOfParcelID = confirmation.PartOfParcelID});//dodato
                partOfParcelRepository.SaveChanges();
                return Created(location, mapper.Map<PartOfParcelConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [Consumes("application/json")]
        [HttpPut]
        public ActionResult<PartOfParcelConfirmationDto> UpdatePartOfParcel(PartOfParcelUpdateDto model)
        {
            try
            {
                var oldPartOfParcelEntity = partOfParcelRepository.GetPartOfParcelById(model.PartOfParcelID);
                //Proveriti da li uopšte postoji prijava koju pokušavamo da ažuriramo.
                if (oldPartOfParcelEntity == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }

                PartOfParcel partOfParcelEntity = mapper.Map<PartOfParcel>(model);

                mapper.Map(partOfParcelEntity, oldPartOfParcelEntity);

                partOfParcelRepository.SaveChanges();

                return Ok(mapper.Map<PartOfParcelConfirmationDto>(oldPartOfParcelEntity));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpDelete("{partOfParcelID}")]
        public IActionResult DeletePartOfParcel(Guid partOfParcelID)
        {
            try
            {
                PartOfParcel model = partOfParcelRepository.GetPartOfParcelById(partOfParcelID);
                if (model == null)
                {
                    return NotFound();
                }
                partOfParcelRepository.DeletePartOfParcel(partOfParcelID);
                partOfParcelRepository.SaveChanges();
                // Status iz familije 2xx koji se koristi kada se ne vraca nikakav objekat, ali naglasava da je sve u redu
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpOptions]
        public IActionResult GetPartOfParcelsOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}
