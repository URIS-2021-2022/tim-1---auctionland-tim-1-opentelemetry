using AdMicroservice.Helpers;
using AdMicroservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Controllers
{
    /// <summary>
    /// Kreirana klasa AuthenticationController
    /// </summary>
    [ApiController]
    [Route("api/ads")]
    [Produces("application/json", "application/xml")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationHelper authenticationHelper;

        /// <summary>
        /// Injektovanje zavisnosti preko kontruktora klase AuthenticationController
        /// </summary>
        /// <param name="authenticationHelper"></param>
        public AuthenticationController(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }

        /// <summary>
        /// Sluzi za autentifikaciju korisnika
        /// </summary>
        /// <param name="principal">Model sa podacima na osnovu kojih se vrši autentifikacija</param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Authenticate(Principal principal)
        {
            //Pokušaj autentifikacije
            if (authenticationHelper.AuthenticatePrincipal(principal))
            {
                var tokenString = authenticationHelper.GenerateJwt(principal);
                return Ok(new { token = tokenString });
            }

            //Ukoliko autentifikacija nije uspela vraća se status 401
            return Unauthorized();
        }
    }
}
