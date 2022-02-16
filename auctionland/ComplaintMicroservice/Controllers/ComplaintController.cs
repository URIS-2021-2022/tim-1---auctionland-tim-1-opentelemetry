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

        [HttpGet]
        [HttpHead]
        public ActionResult<List<ComplaintDto>> GetComplaints(string type, string status)
        {
            var complaints = complaintRepository.GetComplaints(type, status);
            if(complaints == null || complaints.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<ComplaintDto>>(complaints));
        }

        [HttpGet("{ComplaintId}")]
        public ActionResult<ComplaintDto> GetComplaint(Guid complaintId)
        {
            Complaint model = complaintRepository.GetComplaintById(complaintId);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ComplaintDto>(model));
        }

        [HttpDelete("{ComplaintId}")]
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

        [HttpPost]
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

        [HttpPut]
        public ActionResult<ComplaintConfirmationDto> UpdateComplaint(ComplaintUpdateDto newComplaint)
        {
            try
            {
                var oldComplaint = complaintRepository.GetComplaintById(newComplaint.ComplaintId);
                if (oldComplaint == null)
                {
                    return NotFound();
                }
                Complaint complaintEntity = mapper.Map<Complaint>(newComplaint);
                mapper.Map(complaintEntity, oldComplaint);
                complaintRepository.SaveChanges();
                return Ok(mapper.Map<ComplaintConfirmationDto>(oldComplaint));//ispravitiiiiii
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error!");
            }
        }

        [HttpOptions]
        public IActionResult GetComplaintOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
