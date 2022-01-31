using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using UserMicroservice.Data;
using UserMicroservice.Entities;
using UserMicroservice.Models;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
    //[Authorize] //Ovom kontroleru mogu da pristupaju samo autorizovani korisnici
    public class UserController : ControllerBase //Daje nam pristup korisnim poljima i metodama
    {
        private readonly IUserRepository userRepository;
        private readonly LinkGenerator linkGenerator; //Služi za generisanje putanje do neke akcije (videti primer u metodu CreateUser)
        private readonly IMapper mapper;

        //Pomoću dependency injection-a dodajemo potrebne zavisnosti
        public UserController(IUserRepository userRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraća sve prijave ispita za zadate filtere.
        /// </summary>
        /// <param name="firstName">ime korisnika</param>
        /// <param name="lastName">prezime korisnika</param>
        /// <returns>Lista korisnika</returns>
        /// <response code="200">Vraća listu korisnika</response>
        /// <response code="404">Nije pronađena ni jedan jedini korisnik</response>
        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UserDto>> GetUsers(string firstName, string lastName)
        {
            var users = userRepository.GetUsers(firstName, lastName);

            //Ukoliko nismo pronašli ni jednog korisnika vratiti status 204 (NoContent)
            if (users == null || users.Count == 0)
            {
                return NoContent();
            }

            //Ukoliko smo našli neke korisnike vratiti status 200 i listu pronađenih korisnika (DTO objekti)
            return Ok(mapper.Map<List<UserDto>>(users));
        }

        /// <summary>
        /// Vraća jednog korisnika na osnovu ID-ja korisnika.
        /// </summary>
        /// <param name="userId">ID korisnika</param>
        /// <returns></returns>
        /// <response code="200">Vraća traženu korisnika</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))] //Kada se koristi IActionResult
        [HttpGet("{userId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public ActionResult<UserDto> GetUser(Guid userId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var user = userRepository.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UserDto>(user));
        }

        /// <response code="200">Vraća kreiranog korisnika</response>
        /// <response code="500">Došlo je do greške na serveru prilikom kreiranja korisnika</response>
        [HttpPost]
        [Consumes("application/json")]        
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserConfirmationDto> CreateUser([FromBody] UserCreationDto user)
        {
            try
            {
                User userEntity = mapper.Map<User>(user);
                UserConfirmation confirmation = userRepository.CreateUser(userEntity);
                userRepository.SaveChanges(); //Perzistiramo promene

                //Generisati identifikator novokreiranog resursa
                string location = linkGenerator.GetPathByAction("GetUser", "User", new { userId = confirmation.Id });



                //Vratiti status 201 (Created), zajedno sa identifikatorom novokreiranog resursa (korisnika) i samim korisnikom.
                return Created(location, mapper.Map<UserConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                //Ukoliko nastane neka greška na servisu vratiti status 500 (InternalServerError).
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        /// <summary>
        /// Ažurira jednog korisnika.
        /// </summary>
        /// <param name="user">Model korisnika koji se ažurira</param>
        /// <returns>Potvrdu o modifikovanom korisniku.</returns>
        /// <response code="200">Vraća ažuriranog korisnika</response>
        /// <response code="400">Korisnik koji se ažurira nije pronađen</response>
        /// <response code="500">Došlo je do greške na serveru prilikom ažuriranja korisnika</response>
        [HttpPut]
        [Consumes("application/json")]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDto> UpdateUser(UserUpdateDto user)
        {
            try
            {
                //Proveriti da li uopšte postoji korisnik koji pokušavamo da ažuriramo.
                var oldUser = userRepository.GetUserById(user.Id);
                if (oldUser == null)
                {
                    return NotFound(); //Ukoliko ne postoji vratiti status 404 (NotFound).
                }
                User userEntity = mapper.Map<User>(user);

                mapper.Map(userEntity, oldUser); //Update objekta koji treba da sačuvamo u bazi                

                userRepository.SaveChanges(); //Perzistiramo promene
                return Ok(mapper.Map<UserDto>(oldUser));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        /// <summary>
        /// Vrši brisanje jedne korisnika na osnovu ID-ja korisnika.
        /// </summary>
        /// <param name="userId">ID korisnika</param>
        /// <returns>Status 204 (NoContent)</returns>
        /// <response code="204">Korisnik uspešno obrisan</response>
        /// <response code="404">Nije pronađen korisnik za brisanje</response>
        /// <response code="500">Došlo je do greške na serveru prilikom brisanja korisnika</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(Guid userId)
        {
            try
            {
                var user = userRepository.GetUserById(userId);

                if (user == null)
                {
                    return NotFound();
                }

                userRepository.DeleteUser(userId);
                userRepository.SaveChanges();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }
        
        /// <summary>
        /// Vraća opcije za rad sa kreiranjem korisnika
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        public IActionResult GetUserOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
