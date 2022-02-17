﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using AutoMapper;
using UserMicroservice.Data;
using UserMicroservice.Models;
using UserMicroservice.Entities;

namespace UserMicroservice.Controllers
{
    [ApiController]
    [Route("api/userTypes")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    [Authorize] //Ovom kontroleru mogu da pristupaju samo autorizovani korisnici
    public class UserTypeController : ControllerBase //Daje nam pristup korisnim poljima i metodama
    {
        private readonly IUserTypeRepository userTypeRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateUserType)
        private readonly IMapper mapper;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public UserTypeController(IUserTypeRepository userTypeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.userTypeRepository = userTypeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve tipove korisnika.
        /// </summary>
        /// <returns>Lista tipova korisnika</returns>
        /// <response code="200">Vraća listu tipova korisnika</response>
        /// <response code="404">Nije pronađena ni jedan jedini tip korisnika</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UserTypeDto>> GetUserTypes()
        {
            var userTypes = userTypeRepository.GetUserTypes();

            //Ukoliko nismo pronašli ni jednuog tipa korisnika vratiti status 204 (NoContent)
            if (userTypes == null || userTypes.Count == 0)
            {
                return NoContent();
            }

            //Ukoliko smo našli neke tipove korisnika vratiti status 200 i listu pronađenih tipova korisnika (DTO objekti)
            return Ok(mapper.Map<List<UserTypeDto>>(userTypes));
        }

        /// <summary>
        /// Vraća jednog tipa korisnika na osnovu ID-ja tipova korisnika.
        /// </summary>
        /// <param name="userTypeId">ID tip korisnika</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženog tipa korisnika</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserType))] //Kada se koristi IActionResult
        [HttpGet("{userTypeId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public ActionResult<UserTypeDto> GetUserType(Guid userTypeId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var userType = userTypeRepository.GetUserTypeById(userTypeId);

            if (userType == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UserTypeDto>(userType));
        }

        /// <response code="200">Vraća kreiran Tip Korisnika</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja tip korisnika</response>
        [HttpPost]
        [Consumes("application/json")]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserTypeConfirmationDto> CreateUserType([FromBody] UserTypeCreationDto userType)
        {
            try
            {
                UserType userTypeEntity = mapper.Map<UserType>(userType);
                UserTypeConfirmation confirmation = userTypeRepository.CreateUserType(userTypeEntity);
                userTypeRepository.SaveChanges(); //Perzistiramo promene

                //Generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetUserType", "UserType", new { userTypeId = confirmation.UserTypeId });

                //Vratiti status 201 (Created), zajedno sa identifikatorom novokreiranog resursa (tipa korisnika).
                return Created(location, mapper.Map<UserTypeConfirmationDto>(confirmation));
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
        /// Ažurira jednu prijavu ispita.
        /// </summary>
        /// <param name="userType">Model tipa korisnika koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom tipu korisnika.</returns>
        /// <response code="200">Vraća ažuriranog tipa korisnika</response>
        /// <response code="400">Tip korisnika koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja tipa korisnika</response>
        [HttpPut]
        [Consumes("application/json")]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserTypeDto> UpdateUserType(UserTypeUpdateDto userType)
        {
            try
            {
                //Proveriti da li uopšte postoji tip korisnika koji pokušavamo da ažuriramo.
                var oldUserType = userTypeRepository.GetUserTypeById(userType.UserTypeId);
                if (oldUserType == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                UserType userTypeEntity = mapper.Map<UserType>(userType);

                mapper.Map(userTypeEntity, oldUserType); //Update objekta koji treba da sačuvamo u bazi                

                userTypeRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<UserTypeDto>(oldUserType));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jednog tipa korisnika na osnovu ID-ja tipa korisnika.
        /// </summary>
        /// <param name="userTypeId">ID tipa korisnika/param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">tip korisnika uspešno obrisan</response>
        /// <response code="404">Nije pronađen tip korisnika za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja tipa korisnika</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{userTypeId}")]
        public IActionResult DeleteUserType(Guid userTypeId)
        {
            try
            {
                var userType = userTypeRepository.GetUserTypeById(userTypeId);

                if (userType == null)
                {
                    return NotFound();
                }

                userTypeRepository.DeleteUserType(userTypeId);
                userTypeRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
        
        /// <summary>
        /// Vraća opcije za rad sa tipovima korisnika
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        public IActionResult GetUserTypeOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
