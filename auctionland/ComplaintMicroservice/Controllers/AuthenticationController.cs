using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplaintMicroservice.Helpers;
using ComplaintMicroservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintMicroservice.Controllers
{
    [Route("api/complaint")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationHelper authenticationHelper;

        public AuthenticationController(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }

        /// <summary>
        /// Used for user authentication
        /// </summary>
        /// <param name="principal">Model with data</param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public IActionResult Authenticate(Principal principal)
        {
            if (authenticationHelper.AuthenticatePrincipal(principal))
            {
                var tokenString = authenticationHelper.GenerateJwt(principal);
                return Ok(new { token = tokenString });
            }
            return Unauthorized();
        }
    }
}
