using AutoMapper;
using CommissionMicroservice.Data;
using CommissionMicroservice.Entities;
using CommissionMicroservice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommissionMicroservice.Controllers
{
    [ApiController]
    [Route("api/commissions")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    [Authorize] //Ovom kontroleru mogu da pristupaju samo autorizovani korisnici
    public class CommissionController : ControllerBase
    {
        private readonly ICommissionRepository commissionRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public CommissionController(ICommissionRepository commissionRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.commissionRepository = commissionRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpHead]
        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<CommissionDto>> GetCommissions(string presidentId)
        {
            List<Commission> commissions = commissionRepository.GetCommissions(presidentId);
            if(commissions.Count == 0 || commissions == null)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<CommissionDto>>(commissions));
        }

        [HttpGet("{commissionID}")]
        public ActionResult<List<CommissionDto>> GetCommissionById(Guid commissionId)
        {
            Commission commission = commissionRepository.GetCommissionById(commissionId);
            if (commission == null)
            {
                return NoContent();
            }
            return Ok(mapper.Map<CommissionDto>(commission));
        }

        [HttpPost]
        public ActionResult<CommissionConfirmationDto> CreateCommission([FromBody] CommissionCreationDto commissionDto)
        {
            try
            {
                bool validate = Validate(commissionDto);
                commissionDto.CommissionID = Guid.NewGuid();
                Commission commission = mapper.Map<Commission>(commissionDto);
                CommissionConfirmation confirmation = commissionRepository.CreateCommission(commission);
                string location = linkGenerator.GetPathByAction("GetCommissionById", "Commission", new { commissionID = confirmation.CommissionID });
                return Created(location, mapper.Map<CommissionConfirmationDto>(confirmation));
            } 
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [HttpDelete("{commissionID}")]
        public IActionResult DeleteCommission(Guid commissionID)
        {
            try
            {
                Commission commission = commissionRepository.GetCommissionById(commissionID);
                if(commission == null)
                {
                    return NotFound();
                }
                commissionRepository.DeleteCommission(commissionID);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        public ActionResult<CommissionConfirmationDto> UpdateCommission([FromBody] CommissionUpdateDto commissionDto)
        {
            try
            {
                var oldCommission = commissionRepository.GetCommissionById(commissionDto.CommissionID);
                if(oldCommission == null)
                {
                    return NotFound();
                }
                Commission commission = mapper.Map<Commission>(commissionDto);
                mapper.Map(commission, oldCommission);
                return Ok(mapper.Map<CommissionDto>(oldCommission));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }

        [HttpOptions]
        public IActionResult GetCommissionOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        public bool Validate(CommissionCreationDto commissionDto)
        {
            if(commissionDto.President == null)
            {
                return false;
            }
            return true;
        }
    }
}
