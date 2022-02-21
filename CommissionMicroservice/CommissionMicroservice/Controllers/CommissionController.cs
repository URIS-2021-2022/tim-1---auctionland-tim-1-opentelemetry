using AutoMapper;
using CommissionMicroservice.Data;
using CommissionMicroservice.Entities;
using CommissionMicroservice.Models;
using CommissionMicroservice.Models.Exceptions;
using CommissionMicroservice.ServiceCalls;
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
    /// <summary>
    /// Kontroler komisije sa CRUD 
    /// </summary>
    [ApiController]
    [Route("api/commissions")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    [Authorize] //Ovom kontroleru mogu da pristupaju samo autorizovani korisnici
    public class CommissionController : ControllerBase
    {
        private readonly ICommissionRepository commissionRepository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;
        private readonly IUserService userService;

        public CommissionController(ICommissionRepository commissionRepository, IMapper mapper, LinkGenerator linkGenerator, IUserService userService)
        {
            this.commissionRepository = commissionRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.userService = userService;
        }

        /// <summary>
        /// Vraca sve komisije po zadatom predsedniku 
        /// </summary>
        /// <param name="presidentId">Predsednik komisije</param>
        /// <returns>Lista komisija</returns>
        /// <response code="200">Vraća listu komisija</response>
        /// <response code="404">Nije pronađena ni jedna komisija</response>
        [HttpHead]
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<CommissionDto>> GetCommissions(string presidentId)
        {
            List<Commission> commissions = commissionRepository.GetCommissions(presidentId);
            if(commissions.Count == 0)
            {
                return NoContent();
            }
            return Ok(mapper.Map<List<CommissionDto>>(commissions));
        }

        /// <summary>
        /// Vraca jednu komisiju sa prosledjenim ID-jem
        /// </summary>
        /// <param name="commissionId">ID komisije</param>
        /// <returns>Vraca jednu komisiju</returns>
        /// <response code="200">Vraća komisiju sa prosledjenim ID-jem</response>
        /// <response code="404">Nije pronađena ni jedna komisija sa tim ID-jem</response>
        [HttpGet("{commissionID}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<CommissionDto>> GetCommissionById(Guid commissionId)
        {
            Commission commission = commissionRepository.GetCommissionById(commissionId);
            if (commission == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CommissionDto>(commission));
        }

        /// <summary>
        /// Kreira novu komisiju
        /// </summary>
        /// <param name="commissionDto">Model komisije</param>
        /// <returns>Potvrda o kreiranoj komisiji</returns>
        /// <response code="201">Komisija je uspešno kreirana</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja komisije</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CommissionConfirmationDto> CreateCommission([FromBody] CommissionCreationDto commissionDto)
        {
            // komunikacija
            try
            {
                //bool validate = Validate(commissionDto);
                commissionDto.CommissionID = Guid.NewGuid();
                Commission commission = mapper.Map<Commission>(commissionDto);
                CommissionConfirmation confirmation = commissionRepository.CreateCommission(commission);
                commissionRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetCommissionById", "Commission", new { commissionID = confirmation.CommissionID });
                /*
                var userInfo = mapper.Map<UserDto>(commissionDto);
                userInfo.CommissionId = confirmation.CommissionID;
                bool createdUser = userService.GetUserOfCommission(userInfo);
                //Ukoliko iz nekog razloga ne uspe, komisija se briše
                if (!createdUser)
                {
                    commissionRepository.DeleteCommission(confirmation.CommissionID);
                    throw new UserException("Neuspešno kreiranje komisije. Postoji problem sa korisnikom. Molimo kontaktirajte administratora"); //Bacamo izuzetak koji će biti uhvaćen i vraćen kao status 500
                }*/

                return Created(location, mapper.Map<CommissionConfirmationDto>(confirmation));
            } 
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        /// Brisanje komisije sa prosledjenim ID-jem
        /// </summary>
        /// <param name="commissionID">ID komisije</param>
        /// <returns></returns>
        /// <response code="404">Nije pronađena komisija</response>
        /// <response code="204">Komisija sa prosleđenim id-jem je obrisana</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja komisije</response>
        [HttpDelete("{commissionID}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                commissionRepository.SaveChanges();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        /// Modifikacija postojece komisije
        /// </summary>
        /// <param name="commissionDto">Model komisije</param>
        /// <returns>Potvrda o modifikovanoj komisiji</returns>
        /// <response code="200">Vraća ažuriranu komisiju</response>
        /// <response code="404">Komisija koju je potrebno ažurirati nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja komisije</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                return Ok(mapper.Map<CommissionConfirmationDto>(oldCommission));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }

        /// <summary>
        /// Vraca opcije za rad sa komisijama u sistemu
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult GetCommissionOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        /// <summary>
        /// Proveravanje validnosti unesenih podataka
        /// </summary>
        /// <param name="commissionDto">Model komisije</param>
        /// <returns></returns>
        private static bool Validate(CommissionCreationDto commissionDto)
        {
            if(commissionDto.President == null)
            {
                return false;
            }
            return true;
        }
    }
}
