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
    [ApiController]
    [Route("api/complaint")]
    [Produces("application/json", "application/xml")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationHelper authenticationHelper;

        public AuthenticationController(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }

        /// <summary>
        /// Koristi se za autentifikaciju korisnika
        /// </summary>
        /// <param name="principal">Model sa podacima na osnovu kojih se vrši autentifikacija</param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
