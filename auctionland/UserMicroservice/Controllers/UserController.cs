using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMicroservice.Models;
using UserMicroservice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace UserMicroservice.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private object linkGenerator;

        [HttpGet]
        [HttpHead]
        public ActionResult<List<UserResponseDto>> GetUsers()
        {
            return Ok(userService.GetAll());
        }

        [HttpGet("{userId}")]
        public ActionResult<UserResponseDto> GetUserById(Guid id)
        {
            return Ok(userService.GetById(id));
        }

        [HttpPost]
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
        public ActionResult<UserResponseDto> UpdateExamRegistration(UserRequestDto requestDto)
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
        public IActionResult GetExamRegistrationOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
