using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UserMicroservice.Models;
using UserMicroservice.Services;
using Microsoft.AspNetCore.Http;

namespace UserMicroservice.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Produces("application/json", "application/xml")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UserResponseDto>> GetAllUsers()
        {
            var users = userService.GetAll();
            
            if (users == null || users.Count == 0)
            {
                return NoContent();
            }
            
            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<UserResponseDto> GetUserById(Guid id)
        {
            var user = userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserResponseDto> CreateUser([FromBody] UserRequestDto requestDto)
        {
            try
            {
                return Ok(userService.Create(requestDto));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                if (userService.GetById(id) == null)
                {
                    return NotFound();
                }
                userService.Delete(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        public ActionResult<UserResponseDto> UpdateUser(UserRequestDto requestDto)
        {
            try
            {
                return Ok(userService.Update(requestDto));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");
            }
        }

        [HttpOptions]
        public IActionResult GetUserOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
