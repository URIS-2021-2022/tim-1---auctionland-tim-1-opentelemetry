using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ComplaintMicroservice.Data;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ComplaintMicroservice.Controllers
{
    [ApiController]
    [Route("api/complaint")]
    [Produces("application/json", "application/xml")]
    [Authorize]
    //[Consumes("application/json")]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintRepository complaintRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ComplaintController(IComplaintRepository complaintRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.complaintRepository = complaintRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve žalbe po zadatom tipu ili statusu žalbe
        /// </summary>
        /// <param name="type">Tip žalbe</param>
        /// <param name="status">Status žalbe</param>
        /// <returns>Lista žalbi u sistemu</returns>
        /// <response code="200">Vraća listu žalbi</response>
        /// <response code="404">Nije pronađena ni jedna žalba</response>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ComplaintDto>> GetComplaints(string type, string status)
        {
            var complaints = complaintRepository.GetComplaints(type, status);
            if(complaints == null || complaints.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ComplaintDto>>(complaints));
        }

        /// <summary>
        /// Vraća jednu žalbu koja ima prosleđeni id
        /// </summary>
        /// <param name="complaintId">Id žalbe</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženu žalbu</response>
        /// <response code="404">Nije pronađena žalba sa prosleđenim id-jem</response>
        [HttpGet("{complaintId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ComplaintDto> GetComplaint(Guid complaintId)
        {
                Complaint model = complaintRepository.GetComplaintById(complaintId);
                if (model == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<ComplaintDto>(model));
        }

        /// <summary>
        /// Brisanje žalbe koja ima prosleđeni id
        /// </summary>
        /// <param name="complaintId">iId žalbe</param>
        /// <returns></returns>
        /// <response code="404">Nije pronađena žalba</response>
        /// <response code="204">Žalba sa prosleđenim id-jem je obrisana</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja žalbe</response>
        [HttpDelete("{complaintId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteComplaint(Guid complaintId)
        {
            try
            {
                Complaint complaintModel = complaintRepository.GetComplaintById(complaintId);
                if (complaintModel == null)
                {
                    return NotFound();
                }
                complaintRepository.DeleteComplaint(complaintId);
                complaintRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Kreira novu žalbu
        /// </summary>
        /// <param name="complaintCreationDto">Model žalbe</param>
        /// <returns>Potvrda o kreiranoj žalbi</returns>
        /// <remarks>
        /// Primer zahteva za kreiranje nove žalbe \
        /// POST api/complaint
        /// { \
        ///     "submissionDate": "2020-11-13T09:00:00", \
        ///     "reason": "Razlog zalbe", \
        ///     "explanation": "Obrazlozenje zalbe", \
        ///     "dateOfDecision": "2020-11-15T09:00:00", \
        ///     "numberOfDecision": "76", \
        ///     "action": "Public bidding does not go to the second round", \
        ///     "complaintTypeId": "21ad52f8-0281-4241-98b0-481566d25e4f", \
        ///     "complaintTypeName": "Appeal against the Decision on Leasing", \
        ///     "complaintStatusId": "044f3de0-a9dd-4c2e-b745-89976a1b2a36", \
        ///     "complaintStatus": "Rejected" \
        /// } \
        /// </remarks>
        /// <response code="201">Žalba je uspešno kreirana</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja žalbe</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ComplaintConfirmationDto> CreateComplaint([FromBody] ComplaintCreationDto complaintCreationDto)
        {
            try
            {
                Complaint complaint = mapper.Map<Complaint>(complaintCreationDto);
                ComplaintConfirmation confirmation = complaintRepository.CreateComplaint(complaint);
                complaintRepository.SaveChanges();
                string location = linkGenerator.GetPathByAction("GetComplaint", "Complaint", new { complaintId = confirmation.ComplaintId });
                return Created(location, mapper.Map<ComplaintConfirmationDto>(confirmation));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }
        /// <summary>
        /// Izmena postojeće žalbe
        /// </summary>
        /// <param name="newComplaint">Model žalbe</param>
        /// <returns>Potvrda o modifikovanoj žalbi</returns>
        /// <response code="200">Vraća ažuriranu žalbu</response>
        /// <response code="404">Žalba koju je potrebno ažurirati nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja prijave ispita</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ComplaintConfirmationDto> UpdateComplaint(ComplaintUpdateDto newComplaint)
        {
            try
            {
                Complaint oldComplaint = complaintRepository.GetComplaintById(newComplaint.ComplaintId);
                if (oldComplaint == null)
                {
                    return NotFound();
                }
                Complaint complaintEntity = mapper.Map<Complaint>(newComplaint);
                //ComplaintConfirmation confirmation = complaintRepository.UpdateComplaint(complaintEntity);
                mapper.Map(complaintEntity, oldComplaint);
                complaintRepository.SaveChanges();
                return Ok(mapper.Map<ComplaintConfirmationDto>(oldComplaint));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error!");
            }
        }

        /// <summary>
        /// Vraća opcije za rad sa žalbama u sistemu
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetComplaintOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
