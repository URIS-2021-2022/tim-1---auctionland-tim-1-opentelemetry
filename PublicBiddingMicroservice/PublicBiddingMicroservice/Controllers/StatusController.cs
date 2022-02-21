using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using AutoMapper;
using PublicBiddingMicroservice.Models;
using PublicBiddingMicroservice.Entities;
using PublicBiddingMicroservice.Data;
using PublicBiddingMicroservice.ServiceCalls;

namespace PublicBiddingMicroservice.Controllers
{
    [ApiController]
    [Route("api/statuses")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    [Authorize] //Ovom kontroleru mogu da pristupaju samo autorizovani korisnici
    public class StatusController : ControllerBase //Daje nam pristup korisnim poljima i metodama
    {
        private readonly IStatusRepository statusRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateStatus)
        private readonly IMapper mapper;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public StatusController(IStatusRepository statusRepository, LinkGenerator linkGenerator, IMapper mapper, IAddressService addressService)
        {
            this.statusRepository = statusRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve statuse.
        /// </summary>
        /// <returns>Lista statusa</returns>
        /// <response code="200">Vraća listu statusa</response>
        /// <response code="404">Nije pronađena ni jedn jedini status</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<StatusDto>> GetStatuss()
        {
            var statuss = statusRepository.GetStatuss();

            //Ukoliko nismo pronašli ni jednan status vratiti status 204 (NoContent)
            if (statuss == null || statuss.Count == 0)
            {
                return NoContent();
            }

            //Ukoliko smo našli neke statuse vratiti status 200 i listu pronađenih statusa (DTO objekti)
            return Ok(mapper.Map<List<StatusDto>>(statuss));
        }

        /// <summary>
        /// Vraća jedan status na osnovu ID-ja statusa.
        /// </summary>
        /// <param name="statusId">ID statusa</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženi status</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Status))] //Kada se koristi IActionResult
        [HttpGet("{statusId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public ActionResult<StatusDto> GetStatus(Guid statusId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var status = statusRepository.GetStatusById(statusId);

            if (status == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<StatusDto>(status));
        }

        /// <summary>
        /// Kreira jedan status.
        /// </summary>
        /// <response code="200">Vraća kreirani status</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranje statusa</response>
        [HttpPost]
        [Consumes("application/json")]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StatusConfirmationDto> CreateStatus([FromBody] StatusCreationDto status)
        {
            try
            {
                Status statusEntity = mapper.Map<Status>(status);
                StatusConfirmation confirmation = statusRepository.CreateStatus(statusEntity);
                statusRepository.SaveChanges(); //Perzistiramo promene

                //Generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetStatus", "Status", new { statusId = confirmation.StatusId });

                //Vratiti status 201 (Created), zajedno sa identifikatorom novokreiranog resursa (statusa).
                return Created(location, mapper.Map<StatusConfirmationDto>(confirmation));
                //return CreatedAtRoute(); //Druga opcija za vraćanje kreiranog resursa i lokacije
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Ukoliko nastane neka greška na servisu vratiti status 500 (InternalServerError).
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        /// <summary>
        /// Ažurira jedan status.
        /// </summary>
        /// <param name="status">Model prijave statusa koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom statusu.</returns>
        /// <response code="200">Vraća ažurirani status</response>
        /// <response code="400">Status koja se ažurira nije pronađena</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja statusa</response>
        [HttpPut]
        [Consumes("application/json")]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StatusDto> UpdateStatus(StatusUpdateDto status)
        {
            try
            {
                //Proveriti da li uopšte postoji status koji pokušavamo da ažuriramo.
                var oldStatus = statusRepository.GetStatusById(status.StatusId);
                if (oldStatus == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                Status statusEntity = mapper.Map<Status>(status);

                mapper.Map(statusEntity, oldStatus); //Update objekta koji treba da sačuvamo u bazi                

                statusRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<StatusDto>(oldStatus));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jednog statusa na osnovu ID-ja statusa.
        /// </summary>
        /// <param name="statusId">ID statusa</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Status uspešno obrisan</response>
        /// <response code="404">Nije pronađen statusa za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja statusa</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{statusId}")]
        public IActionResult DeleteStatus(Guid statusId)
        {
            try
            {
                var status = statusRepository.GetStatusById(statusId);

                if (status == null)
                {
                    return NotFound();
                }

                statusRepository.DeleteStatus(statusId);
                statusRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
        
        /// <summary>
        /// Vraća opcije za rad sa statusima.
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        public IActionResult GetStatusOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
